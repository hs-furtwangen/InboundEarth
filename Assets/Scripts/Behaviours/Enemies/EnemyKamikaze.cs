using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikaze : Enemy {

	GameObject navigator;
	float navigatorSpeed = 0;

	// Use this for initialization
	void Awake () {
		Speed = 0.3f;
		Range = 0.01f;
		AttackDamage = 40; //Defined in Move - workaround
		MaxHealth = 8;
		Health = 8;
		CoolDown = Mathf.Infinity;
		FixedTarget = true;
		Target = GameState.GetEarth();
		//SelectTargetCustomRange(5.0f);

		//Navigator Mess
		navigator = new GameObject();
		navigator.transform.position = this.transform.position;
		navigator.transform.rotation = this.transform.rotation;

		float rx = Random.Range(0.4f,0.7f);
		float ry = Random.Range(0.4f,0.7f);

		rx = Random.Range(0f,1f) > 0.5 ? rx : -rx;
		ry = Random.Range(0f,1f) > 0.5 ? ry : -ry;

		navigator.transform.Translate(new Vector3(rx, ry, -1.3f), Space.Self);
	}

	public override void Move() {
		//Move navigator towards target
		navigatorSpeed += 0.5f * Time.deltaTime;
		navigator.transform.LookAt(Target.transform);
		navigator.transform.Translate(Vector3.forward * navigatorSpeed * Time.deltaTime);

        if (!InRange())
        {
            transform.LookAt(navigator.transform);
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        } else
        {
        	Target.Hit(AttackDamage);
			Destroy(navigator);
			this.Hit(90000);
        }
	}
}