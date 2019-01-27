/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;

public class Asteroid : Enemy
{

	#region Variables
	Vector3 direction;

	#endregion

	#region Unity Methods
	void Awake () {
		Speed = Random.Range(0.1f,0.5f);
		Range = 0.04f;
		AttackDamage = 100;
		MaxHealth = 40;
		Health = 40;

		direction = new Vector3(Random.Range(-0.3f,0.3f),Random.Range(-0.3f,0.3f),Random.Range(-0.3f,0.3f));
	}

	
	new void Update () 
	{
		this.transform.Translate(direction * Time.deltaTime);

		foreach (Collider collider in Physics.OverlapSphere(this.transform.position, Range / 2))
		{
			Satellite sat = collider.gameObject.GetComponent<Satellite>();
			if(sat || collider.gameObject == GameState.GetEarth()){ 
				collider.gameObject.GetComponent<AttackTarget>().Hit(this.AttackDamage);
				Destroy(this);
			}
		}
	}

	#endregion

	#region otherMethods

	#endregion
}
