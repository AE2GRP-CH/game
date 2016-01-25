using UnityEngine;
using System.Collections;
using UnityEngine.Networking;




public class MainMenu : MonoBehaviour {

	public string ipAddress;
	public string port;
	NetworkClient client;

	public void startGame()
	{
		LobbyManager manager = new LobbyManager ();
		manager.startServer ();
		Application.LoadLevel ("select_map");
	}
	public void joinGame()
	{
		// Check if there is a room

		// Has the game Started?

		// If no, send the player to the Waiting For Players page

		// If yes, give error message.
		Debug.Log (string.Format ("join button clicked"));
		client = new NetworkClient();
		client.Connect ("192.168.1.103", 4444);
		client.RegisterHandler(MsgType.Connect, OnConnected);
		client.RegisterHandler(MsgType.Error, OnError);
		client.RegisterHandler (Message.GET_HAS_ROOM, OnGetRoom);
	}

	void OnConnected(NetworkMessage msg)
	{
		Debug.Log (string.Format("Connected to Server"));
		connectionIdMessage connIdMsg = new connectionIdMessage ();
		connIdMsg.setConnId (client.connection.connectionId);
		client.Send(Message.GET_HAS_ROOM, connIdMsg);
	}

	void OnGetRoom(NetworkMessage msg)
	{
		Debug.Log (string.Format("Get Room availability status"));
		bool hasRoom = msg.ReadMessage<hasRoomMessage> ().getHasRoom ();
		if (hasRoom) {
			Application.LoadLevel("select_map");
		} else {
			showErrorDialog(true);
		}
	}

	void OnError(NetworkMessage msg)
	{
		Debug.Log (string.Format ("error occured"));
		showErrorDialog (false);		
	}

	void showErrorDialog(bool hasNoRoom)
	{
		if (hasNoRoom) {

		}
	}

	public void help()
	{
		Application.LoadLevel ("Help");

	}
	public void quitGame()
	{
		Application.Quit ();
	}

}
