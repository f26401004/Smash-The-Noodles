using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nakama;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System;

public class Socket : MonoBehaviour {
    private ConcurrentQueue<IMatchState> states = new ConcurrentQueue<IMatchState> ();
    private ISession session;
    private string deviceId;
    private ISocket socket;
    private string matchId;
    private int memberNumber;
    public InputField id;
    public EnemyMovement enemyPrefab;
    public Dictionary<string, EnemyMovement> enemies = new Dictionary<string, EnemyMovement>();
    private bool isConnected = false;
    public bool isLeader = false;
	// Set of all items.
    private Dictionary<string, Item> itemSet = new Dictionary<string, Item>();

    // Item Prefabs
    public Item Pizza;

    private string position => $"{{\"x\":\"{transform.position.x}\",\"y\":\"{transform.position.y}\"}}";
    // Start is called before the first frame update
    async void Start () {
        // config the member number of a game
        this.memberNumber = 2;

        var client = new Client ("http", "140.114.77.40", 7350, "defaultkey");
        // get the current device id
        this.deviceId = PlayerPrefs.GetString ("nakama.deviceid");
        if (string.IsNullOrEmpty (this.deviceId)) {
            this.deviceId = SystemInfo.deviceUniqueIdentifier;
            PlayerPrefs.SetString ("nakama.deviceid", this.deviceId);
        }
        // get the session
        this.session = await client.AuthenticateDeviceAsync (this.deviceId);

        this.socket = client.NewSocket ();
        this.socket.Connected += () => {
            Debug.Log ("Socket connected.");
        };
        this.socket.Closed += () => {
            Debug.Log ("Socket closed.");
        };
        this.socket.ReceivedMatchState += newState => {
            var enc = System.Text.Encoding.UTF8;
            var content = enc.GetString (newState.State);
            Debug.LogFormat ("Received: {0}, {1}", newState.OpCode, content);
            states.Enqueue(newState);
        };
        await this.socket.ConnectAsync (this.session);

        // test send message
        // await this.JoinMatch ();

    }

    private void Update () {
		if (isConnected)
		{
            this.sendMessage(1, position);	
        }
        
        while (states.TryDequeue (out var state)) {
            var payload = System.Text.Encoding.UTF8.GetString (state.State);
            Debug.Log ($"{state.OpCode}, {payload}");

            string enemyId = state.UserPresence.UserId;
            if (!enemies.ContainsKey(enemyId))
            {
                var newEnemy = Instantiate(enemyPrefab);
                enemies.Add(enemyId, newEnemy);
            }

            JsonData decoded = JsonMapper.ToObject(payload);

			// Move
            if (state.OpCode == 1)
			{
                float x = float.Parse(decoded["x"].ToString());
                float y = float.Parse(decoded["y"].ToString());
                enemies[enemyId].WalkTo(new Vector3(x, y));
			}
            // Pick. 只有一般玩家需要處理這個訊息
            // id: item_id
            else if (state.OpCode == 2 && !isLeader)
			{
                // TODO: Make someonee pipck up something
                Item itemPicked = itemSet[decoded["id"].ToString()];
                itemSet.Remove(decoded["id"].ToString());

				if (enemies.ContainsKey(state.UserPresence.UserId))
				{

				}
				else
				{

				}
            }
            // Trypick. 只有房主需要處理這個訊息
            // id: item_id
            else if (state.OpCode == 3 && isLeader)
			{
				// When Trypick, leader deterimnes whether item is present
				if (itemSet.ContainsKey(decoded["id"].ToString()))
                {
                    // Send actual pick
                    socket.SendMatchStateAsync(matchId, 2, $"{{\"picker\":\"{state.UserPresence.UserId}\",\"item\":{decoded["id"].ToString()}}}");
                    // TODO: Make someonee pipck up something
                    Item itemPicked = itemSet[decoded["id"].ToString()];
                    itemSet.Remove(decoded["id"].ToString());

                    if (enemies.ContainsKey(state.UserPresence.UserId))
                    {

                    }
                    else
                    {

                    }
                }
			}
            // 物品因為交換被放下
            // id: item_id, tag: item_type, position: position
            else if (state.OpCode == 6)
            {
                // TODO: 加入 ITEM SET
            }
            // 新物品，由房主生成
            // id: item_id, tag: item_type, position: position
            else if (state.OpCode == 11)
			{
				// TODO: 生成物品
				//decoded[]
			}
        }
    }

	private void GenerateItem(string type, Vector3 position, string key)
	{

	}

    public async void CreateMatch () {
        var match = await socket.CreateMatchAsync ();
        Debug.LogFormat ("New match with id '{0}'.", match.Id);
        this.matchId = match.Id;
        // JoinMatch (match.Id);
        match = await socket.JoinMatchAsync (matchId);
        // send current position to opponent
        this.sendMessage (1, position);
        isConnected = true;
        isLeader = true;
    }

    public async void JoinMatch () {
        var matchId = id.text;
        if (matchId.Length == 0) {
            return;
        }
        var match = await socket.JoinMatchAsync (matchId);
        this.matchId = match.Id;
        // send current position to opponent
        this.sendMessage (1, position);
        isConnected = true;
    }

    // public async void JoinMatch (string matchId) {
    //     var match = await socket.JoinMatchAsync (matchId);
    //     id.text = matchId;
    //     await socket.SendMatchStateAsync (matchId, 1, "{\"hello\":\"world\"}");
    // }

    // async Task JoinMatch () {

    //     var query = "*";
    //     var minCount = this.memberNumber;
    //     var maxCount = this.memberNumber;
    //     var matchmakerTicket = await this.socket.AddMatchmakerAsync (query, minCount, maxCount);

    //     this.socket.ReceivedMatchmakerMatched += async matched => {
    //         this.matchId = matched.MatchId;
    //         Debug.LogFormat ("Received: {0}", matched);
    //         var opponents = string.Join (",\n  ", matched.Users);
    //         Debug.LogFormat ("Matched opponents: [{0}]", opponents);
    //         // join the match automatically
    //         await this.socket.JoinMatchAsync (matched);
    //         sendMessage ();
    //     };

    //     this.socket.ReceivedMatchState += newState => {
    //         var enc = System.Text.Encoding.UTF8;
    //         var content = enc.GetString (newState.State);
    //         Debug.LogFormat ("Received: {0}, {1}", newState.OpCode, content);
    //         states.Enqueue (newState);
    //     };
    // }

    void sendMessage (int opcode, string value) {
        socket.SendMatchStateAsync (this.matchId, opcode, value);
    }
}