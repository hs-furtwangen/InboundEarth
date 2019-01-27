using UnityEngine;

public abstract class Projectile : MonoBehaviour {

	protected float Speed;
	protected float Radius;
	protected bool IsEnemyProjectile;
	public AttackTarget Target;
	public AttackingObject Origin;
	int Damage;

	void Start () {
		Damage = Origin.GetAttackDamage();
	}
	
	// Update is called once per frame
	void Update () {
		//Move towards target
		if (Target != null) {
			transform.LookAt(Target.transform);
			//Hit when near target
			if (Vector3.Distance(this.transform.position, Target.transform.position) < Radius) {
				Target.Hit(Damage); //TODO: Check if the origin is a thing.
				//Sound etc
				Destroy(this.gameObject);
			}
			transform.Translate(Vector3.forward * Speed * Time.deltaTime);
		} else {
			Destroy(this.gameObject);
		}

		//TODO: Destroy after time
	}
}
