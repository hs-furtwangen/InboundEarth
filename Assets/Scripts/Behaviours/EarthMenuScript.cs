/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class EarthMenuScript : MonoBehaviour
{
	Vector3 mainMenuPosition = new Vector3(0,1.2f,1.2f);
	Vector3 creditsMenuPosition = new Vector3(0,0.5f,1f);
	Vector3 gamePosition = new Vector3(0,1.6f,0);
	Vector3 nextPostition;
	string nextScene;
	bool toNextScene = false;
	void Start () 
	{
		nextPostition = transform.position;
	}
	
	void Update () 
	{
		transform.position = Vector3.Lerp(this.transform.position, nextPostition, Time.deltaTime / 2);
		transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0,0,0), Time.deltaTime / 2);
		if(toNextScene && Vector3.Distance(transform.position, nextPostition) < 0.01){
			SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
		}
	}

	public void goTo(string scene){
		switch(scene){
			case "MainMenu":
				nextPostition = mainMenuPosition;
				nextScene = "MainMenu";
				toNextScene = true;
			break;
			case "MainCredits":
				nextPostition = creditsMenuPosition;
				nextScene = "MainCredits";
				toNextScene = true;
			break;
			case "MainGame":
				nextScene = "MainGame";
				nextPostition = gamePosition;
				toNextScene = true;
			break;
		}
	}
}
