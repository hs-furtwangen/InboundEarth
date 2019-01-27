using UnityEngine;

public class EnemyBase : Enemy {

	// Use this for initialization
	void Awake () {
		Speed = 0.4f;
		Range = 0.6f;
		AttackDamage = 5;
		CoolDown = 2;
		MaxHealth = 10;
		Health = 10;
	}
}
