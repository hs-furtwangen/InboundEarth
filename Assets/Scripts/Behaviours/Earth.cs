using UnityEngine;

public class Earth : AttackTarget {
	public float rotSpeed = 3;
	public const float radius = 0.24f;

	void Start() {
		MaxHealth = 1000;
		Health = 1000;
		GameState.AddScrap(100);
	}
	
	void Update () {
		Rotate();
	}

	void Rotate(){
		transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
	}
}