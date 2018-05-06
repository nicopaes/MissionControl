using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	bool gameHasEnded = false;

	public void Menu(string input)
	{
		
		Debug.Log("Input");
		switch (input.ToLower())
		{
			case "crew":
				SceneManager.LoadScene ("Credits");
				Debug.Log ("CREW MEMBERS");
				break;
			case "access":
				SceneManager.LoadScene ("SpaceStation");
				Debug.Log ("ACCESS GRANTED");
				break;
			case "abort":
				Application.Quit();
				break;
		}
	}

	public void StartGame(string input)
	{
		switch (input.ToLower ()) 
		{
			case "launch":
				SceneManager.LoadScene ("MovementTest");
				Debug.Log ("LAUNCHING");
				break;

			case "back":
				SceneManager.LoadScene ("MainMenu");
				Debug.Log ("RETURN");
				break;
		}

	}

	public void EndGame()
	{
		if (gameHasEnded == false) 
		{
			gameHasEnded = true;
			SceneManager.LoadScene ("ResultScreen");
			Debug.Log ("GAME OVER");
		}
	}
}
