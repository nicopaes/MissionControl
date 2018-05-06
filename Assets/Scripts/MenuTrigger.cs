using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTrigger : MonoBehaviour {

	public void SpaceStation () 
	{
		SceneManager.LoadScene ("SpaceStation");
		Debug.Log ("ACCESS GRANTED");
	}

	public void CreditScreen () 
	{
		SceneManager.LoadScene ("Credits");
		Debug.Log ("CREW MEMBERS");
	}

	public void Exit () 
	{
		Application.Quit();
	}

	public void MainMenu () 
	{
		SceneManager.LoadScene ("MainMenu");
		Debug.Log ("RETURN");
	}

	public void StartGame () 
	{
		SceneManager.LoadScene ("MovementTest");
		Debug.Log ("LAUNCHING");
	}

}
