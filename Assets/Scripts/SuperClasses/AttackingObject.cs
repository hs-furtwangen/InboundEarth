/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;

public class AttackingObject : MovingObject
{

	#region Variables
	protected AttackTarget Target;
	public Projectile projectile;
	protected int AttackDamage;
	protected float CoolDown;
	protected float CurrentCoolDown;
	protected float Range;
	#endregion

	#region Unity Methods
	
	protected virtual void LateUpdate () 
	{
		CurrentCoolDown += Time.deltaTime;
	}

	#endregion

	#region otherMethods
	public virtual void Attack(){
		//spawn projectile and set target
		if (CurrentCoolDown > CoolDown) {
			Projectile pr = Instantiate(projectile, this.transform.position, Quaternion.identity);
			pr.Target = Target;
			pr.Origin = this.GetComponent<AttackingObject>();

			CurrentCoolDown = 0;
		}
	}

	public int GetAttackDamage() {
		return AttackDamage;
	}

	#endregion
}
