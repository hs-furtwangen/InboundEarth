using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour {
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}

	public void LoadMainCreds()
	{
		SceneManager.LoadScene("MainCredits", LoadSceneMode.Single);
	}

	public void LoadMainGame()
	{
		SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
	}
}
