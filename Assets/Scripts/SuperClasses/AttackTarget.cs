/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;

public class AttackTarget : MonoBehaviour
{

	#region Variables
	protected int Health;
	protected int MaxHealth;
	#endregion

	#region Unity Methods

	Explosion explosion;
	
	void Update () 
	{
		
	}

	#endregion

	#region otherMethods
	public void Hit(int damage){
		this.Health = Mathf.Clamp(this.Health - damage, 0, MaxHealth);
		if(this.Health <= 0){
			SpaceDebris.CreateNew(this.transform.position, MaxHealth);
			explosion = Resources.Load<Explosion>("Prefabs/Explosion");
			var t = Instantiate(explosion, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}

	public int getHealth(){
		return Health;
	}
	public int getHealthMax(){
		return MaxHealth;
	}
	#endregion
}
