using UnityEngine;

public class Spawner : MonoBehaviour {

	public float frequency; //0 - 1
	public float radius;
	public float lifeTime = 10;

	
	void Awake() {
		SpawnManager.AddSpawnerToList(this);
		frequency = Mathf.Clamp(frequency, 0, 1);
		InvokeRepeating("spawn", 1.0f, 0.5f);
		Invoke("end", lifeTime);
	}

	void spawn () {
		if (SpawnManager.CountEnemies() > SpawnManager.MaxEnemies) {
			return;
		}

		if (Random.Range(0f, 1f) < frequency) {
			Vector3 pos = transform.position;
			pos += new Vector3(Random.Range(-radius, radius),Random.Range(-radius, radius),Random.Range(-radius, radius));
			Instantiate(chooseEnemy(), pos, Quaternion.identity);
		}
	}

	Enemy chooseEnemy() {
		string path = "Prefabs/Enemies/EnemyBasic";

		//Spawner chances TODO: add difficulty level instead
		float rDif = Random.Range(0f, GameState.Difficulty);
		float r = Random.Range(0f, 1f);

		if (rDif > 0.3 && r > 0.8f) {
			path = "Prefabs/Enemies/EnemyShield";
		}
		if (rDif > 0.55 && r > 0.85f) {
			path = "Prefabs/Enemies/EnemyMotherShip";
		}
		Enemy selectedEnemy = Resources.Load<Enemy>(path);
		return selectedEnemy;
	}

	void end() {
		SpawnManager.RemoveSpawnerFromList(this);
		Destroy(this.gameObject);
	}
}
