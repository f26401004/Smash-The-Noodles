using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nakama;
using UnityEngine;

public class Socket : MonoBehaviour {
    private ISession session;
    private string deviceId;
    private ISocket socket;
    private string matchId;
    private int memberNumber;
    // Start is called before the first frame update
    async void Start () {
        // config the member number of a game
        this.memberNumber = 2;

        var client = new Client ("http", "2c49e37d.ngrok.io", 80, "defaultkey");
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
        await this.socket.ConnectAsync (this.session);

        // // create match id
        // var match = await this.socket.CreateMatchAsync ();
        // this.matchId = match.Id;
        // Debug.LogFormat ("New match with id {0}", this.matchId);

        // test send message
        await this.JoinMatch ();
    }

    async Task JoinMatch () {
        var query = "*";
        var minCount = this.memberNumber;
        var maxCount = this.memberNumber;
        var matchmakerTicket = await this.socket.AddMatchmakerAsync (query, minCount, maxCount);

        this.socket.ReceivedMatchmakerMatched += async matched => {
            Debug.LogFormat ("Received: {0}", matched);
            var opponents = string.Join (",\n  ", matched.Users);
            Debug.LogFormat ("Matched opponents: [{0}]", opponents);
            // join the match automatically
            await this.socket.JoinMatchAsync (matched);
            sendMessage ();
        };

        this.socket.ReceivedMatchState += newState => {
            var enc = System.Text.Encoding.UTF8;
            var content = enc.GetString (newState.State);
            Debug.LogFormat ("Received: {0}, {1}", newState.OpCode, content);
        };
    }

    void sendMessage () {
        socket.SendMatchStateAsync (this.matchId, 1, "{\"hello\":\"world\"}");
    }

    // Update is called once per frame
    void Update () {

    }
}