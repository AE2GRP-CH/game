using UnityEngine;
using System.Collections;
using UnityEngine.Networking;



/*
  Class: connectionCountMessage

  A MessageBase subclass to tell the number of players
  
*/

public class connectionCount : MessageBase
{
	private int playerCount;
	public void newMessage(int val){ playerCount = val; }
	public int getMessage(){ return playerCount; }
}

/*

  Class: hasRoomMessage

  A MessageBase subclass to tell the availability of a room.

*/

public class hasRoomMessage : MessageBase
{
	private bool hasRoom;
	public void setHasRoom(bool val){ hasRoom = val; }
	public bool getHasRoom(){ return hasRoom; }
}

public class connectionIdMessage : MessageBase
{
	private int connectionId;
	public void setConnId(int val){ connectionId = val; }
	public int getConnId(){ return connectionId; }
}

/*
  	Class: mapMessage
  	
  	A MessageBase subclass to toll players a changed in map choosen by host
*/

public class mapMessage : MessageBase
{
	private int mapIntValue;
	/*
 		Function: newMessage

		Create new message

		Parameters:

			val - the int value of the map 

		Returns:

            void
	*/
	public void newMessage(int val){ mapIntValue = val; }
	
	/*
 		Function: getMessage

		Get the value of the map int

		Parameters:

			none

		Returns:

			the map int value send by host
	*/
	
	
	
	public int getMessage(){ return mapIntValue;}
}

/*
	Class: LobbyManager
	A class that acts as the LobbyManager
*/

public class LobbyManager{

	private bool hasRoomAvailable;
	private int currentHost;

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
		NetworkServer.Listen ("192.168.1.103",4444);
		NetworkServer.RegisterHandler (Message.CHANGE_MAP, OnChangeMap);
		NetworkServer.RegisterHandler (Message.NUM_OF_PLAYERS, OnRequestPlayerCount);
		NetworkServer.RegisterHandler(Message.SET_HAS_ROOM, OnSetHasRoom);
		NetworkServer.RegisterHandler (Message.GET_HAS_ROOM, OnGetRoom);
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
		int mapValue = msg1.ReadMessage<mapMessage> ().getMessage ();
		mapMessage msg = new mapMessage ();
		msg.newMessage (mapValue);
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
		connectionCount msg = new connectionCount ();
		msg.newMessage (playerCount1);
		NetworkConnection localConn = NetworkServer.localConnections [0];
		NetworkServer.SendToClient (localConn.connectionId, Message.NUM_OF_PLAYERS, msg);
	}

	/*
        Function: OnSetHasRoom

		Receive availability status of room from host and saved it for client to check

        Parameters: 

           msg

        Returns:

           void
     */

	void OnSetHasRoom(NetworkMessage msg)
	{
		bool hasRoom1 = msg.ReadMessage<hasRoomMessage> ().getHasRoom();
		hasRoomAvailable = hasRoom1;
	}

	void OnGetRoom(NetworkMessage msg)
	{
		int connId = msg.ReadMessage<connectionIdMessage> ().getConnId ();
		hasRoomMessage msg1 = new hasRoomMessage ();
		msg1.setHasRoom (hasRoomAvailable);
		NetworkServer.SendToClient (connId, Message.GET_HAS_ROOM, msg1);

	}








}
