using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	static List<Spawner> SpawnerList = new List<Spawner>();
	public static int MaxEnemies = 300;

	float DifficultyAccel = 300;
	

	public static void AddSpawnerToList(Spawner sp) {
		SpawnerList.Add(sp);
	}

	public static void RemoveSpawnerFromList(Spawner sp) {
		SpawnerList.Remove(sp);
	}

	public static int CountEnemies() {
		return SpawnerList.Count;
	}

	// Use this for initialization
	void Start () {
		//DifficultyAccel = 10;
		InvokeRepeating("AddSpawner", 1, 5);
		InvokeRepeating("printDiff", 5f, 5f);
		Invoke("SpawnBoss", 300);
	}

	void AddSpawner() {
		Vector3 RandomPoint = Random.onUnitSphere * 5;
		Spawner spawner = Resources.Load<Spawner>("Prefabs/Enemies/Spawner");

		spawner.lifeTime = 5 + GameState.Difficulty * 30;
		spawner.radius = Random.Range(.5f, 2f);
		spawner.frequency = GameState.Difficulty * 0.4f;

		Instantiate(spawner, RandomPoint, Quaternion.identity);
	}
	void Update() {
		GameState.Difficulty = -1 / (Time.time/DifficultyAccel + 1) + 1;
	}

	void SpawnBoss() {
		GameObject en = Resources.Load<GameObject>("Prefabs/Enemies/EnemyBoss");
		Instantiate(en, Random.onUnitSphere * 10, Quaternion.identity);
	}

	void printDiff() {Debug.Log("Difficulty:" + GameState.Difficulty);}
}
