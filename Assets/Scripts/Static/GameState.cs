using UnityEngine;

public static class GameState {

	static Earth earth = GameObject.FindGameObjectWithTag("Earth").GetComponent<Earth>();
	static int Scrap;
	public static float Difficulty;
	
	// Update is called once per frame
	public static Earth GetEarth () {
		return earth;
	}

	public static int GetScrap() {
		return Scrap;
	}

	//Spend scrap and return true or return false if not enough in bank
	public static bool SpendScrap(int value) {
		if (Scrap >= value) {
			Scrap -= value;
			Debug.Log("Spending scrap: " + value);
			return true;
		}
		return false;
	}

	//Spend scrap and return true or return false if not enough in bank
	public static bool TrySpendScrap(int value) {
		Debug.Log("Try spending scrap");
		if (Scrap >= value) {
			return true;
		}
		return false;
	}

	public static void AddScrap(int value) {
		Scrap += value;
	}
}
