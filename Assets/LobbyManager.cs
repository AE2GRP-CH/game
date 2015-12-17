using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public struct mapMessage
{
	public int mapIntValue;
}

public struct connectionCount
{
	public int playerCount;
}

/*
	Class: LobbyManager
	A class that acts as the LobbyManager
*/

public class LobbyManager : MonoBehaviour {


	/*
	  Function: startServer

	  Create and instance of a NetworkServer and register handler for events

      Parameters:

          None

      Returns:

          void
	*/


	public void startServer()
	{
		NetworkServer.RegisterHandler (Message.CHANGE_MAP, OnChangeMap);
		NetworkServer.RegisterHandler (Message.NUM_OF_PLAYERS, OnRequestPlayerCount);
	}

	/*
		Function: OnChangeMap

		Receives map change info from host and send to all clients

		Parameters:

			msg1 - the message received from the caller
			
		Returns:

			void
	*/
	

	void OnChangeMap(NetworkMessage msg1)
	{
		int mapValue = msg1.ReadMessage<mapMessage> ();
		mapMessage msg;
		msg.mapIntValue = mapValue;
		NetworkServer.SendToAll(Message.CHANGE_MAP,msg);
	}

	/*
        Function: OnRequestPlayerCount

		Send number of players connected to the host

        Parameters: 

           msg1

        Returns:

           void
     */

	void OnRequestPlayerCount(NetworkMessage msg1)
	{
		int playerCount1 = NetworkServer.connections.Count;
		connectionCount msg;
		msg.playerCount = playerCount1;
		NetworkConnection localConn = NetworkServer.localConnections [0];
		NetworkServer.SendToClient (localConn.connectionId, Message.NUM_OF_PLAYERS, msg);
	}








}
