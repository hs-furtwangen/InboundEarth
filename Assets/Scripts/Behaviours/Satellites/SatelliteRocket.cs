/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;

public class SatelliteRocket : Satellite
{

	#region Variables

	#endregion

	#region Unity Methods

	void Awake () 
	{
		this.Health = 100;
		this.MaxHealth = 100;
		this.PreferredTarget = EPreferredTarget.CLOSEST;
		this.Range = 0.6f;
		this.CoolDown = 1f;
		this.AttackDamage = 5;
	}
	
	new void Update () 
	{
		base.Update();
		Attack();
	}

	#endregion

	#region otherMethods

	new void Attack(){
		ChooseTarget();
		if(!Target) return;
		// this.transform.LookAt(Target.transform,this.transform.right);
		transform.forward = Vector3.Lerp(transform.forward, (Target.transform.position - transform.position).normalized, Time.deltaTime * 10);
		base.Attack();
	}

	#endregion
}
