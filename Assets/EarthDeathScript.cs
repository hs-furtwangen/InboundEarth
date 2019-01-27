/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EarthDeathScript : MonoBehaviour
{
	void OnDestroy(){
		this.GetComponent<AudioSource>().Play();
		StartCoroutine("EndTheGame");
	}

	IEnumerator EndTheGame(){
		yield return new WaitForSeconds(5);
		SceneManager.LoadSceneAsync("MainMenu");
	}
}
