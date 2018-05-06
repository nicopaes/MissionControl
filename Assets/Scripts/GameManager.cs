using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	bool gameHasEnded = false;

	public void Crew()
	{
		SceneManager.LoadScene ("Credits");
		Debug.Log ("CREW MEMBERS");
	}

	public void Station()
	{
		SceneManager.LoadScene ("SpaceStation");
		Debug.Log ("ACCESS GRANTED");
	}

	public void StartGame()
	{
		SceneManager.LoadScene ("MovementTest");
		Debug.Log ("LAUNCHING");
	}

	public void GameQuit()
	{
		Application.Quit;
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
