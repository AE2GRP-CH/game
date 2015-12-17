using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void startGame()
	{

	}
	public void joinGame()
	{
		// Check if there is a room

		// Has the game Started?

		// If no, send the player to the Waiting For Players page

		// If yes, give error message.
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
