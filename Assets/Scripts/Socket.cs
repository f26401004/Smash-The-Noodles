using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nakama;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

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

    private string position => $"{{\"x\":\"{transform.position.x}\",\"y\":\"{transform.position.y}\"}}";
    // Start is called before the first frame update
    async void Start () {
        // config the member number of a game
        this.memberNumber = 2;

        var client = new Client ("http", "b6901bd0.ngrok.io", 80, "defaultkey");
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

            if (state.OpCode == 1)
			{
                float x = float.Parse(decoded["x"].ToString());
                float y = float.Parse(decoded["y"].ToString());
                enemies[enemyId].WalkTo(new Vector3(x, y));
			}
        }
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