using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using SocketIO;

public class NetworkManager : MonoBehaviour {

	public static NetworkManager instance;
	public Canvas canvas;
	public SocketIOComponent socket;
	public GameObject player;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		// subscribe to all the various websocket events
		socket.On("other player connected", OnOtherPlayerConnected);
		socket.On("play", OnPlay);
		socket.On("other player disconnected", OnOtherPlayerDisconnect);

        Debug.Log("I AM HEREEEE 123123");
        JoinGame();
       
    }

	public void JoinGame()
	{
        Debug.Log("I AM HEREEEE");
		StartCoroutine(ConnectToServer());
	}

	#region Commands

	IEnumerator ConnectToServer()
	{
		yield return new WaitForSeconds(0.5f);

		socket.Emit("player connect");

		yield return new WaitForSeconds(1f);

		string playerName = "Rabbot";
		PlayerJSON playerJSON = new PlayerJSON(playerName);
		string data = JsonUtility.ToJson(playerJSON);
		socket.Emit("play", new JSONObject(data));
	}

	#endregion

	#region Listening

	void OnOtherPlayerConnected(SocketIOEvent socketIOEvent)
	{
		print("Someone else joined");
		string data = socketIOEvent.data.ToString();
		UserJSON userJSON = UserJSON.CreateFromJSON(data);
        GameObject o = GameObject.Find(userJSON.name) as GameObject;
        if (o != null)
        {
            return;
        }
        GameObject p = Instantiate(player) as GameObject;

    }

	void OnPlay(SocketIOEvent socketIOEvent)
	{
		print("you joined");
		string data = socketIOEvent.data.ToString();
		UserJSON currentUserJSON = UserJSON.CreateFromJSON(data);
    }

	void OnOtherPlayerDisconnect(SocketIOEvent socketIOEvent)
	{
		print("user disconnected");
		string data = socketIOEvent.data.ToString();
		UserJSON userJSON = UserJSON.CreateFromJSON(data);
		Destroy(GameObject.Find(userJSON.name));
	}

	#endregion

	#region JSONMessageClasses

	[Serializable]
	public class PlayerJSON
	{
		public string name;

        public PlayerJSON(string _name)
        {
            name = _name;
        }
    }


	[Serializable]
	public class UserJSON
	{
		public string name;

		public static UserJSON CreateFromJSON(string data)
		{
			return JsonUtility.FromJson<UserJSON>(data);
		}
	}

	#endregion
}