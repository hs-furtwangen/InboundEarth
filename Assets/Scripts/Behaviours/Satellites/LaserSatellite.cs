/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;

public class LaserSatellite : Satellite
{

	#region Variables

	#endregion

	#region Unity Methods

	void Awake(){
		this.Health = 80;
		this.MaxHealth = 80;
		this.PreferredTarget = EPreferredTarget.CLOSEST;
		this.Range = 0.8f;
		this.CoolDown = 0.15f;
		this.AttackDamage = 2;
	}

	new void Start () 
	{
		base.Start();
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
		transform.up = Vector3.Lerp(transform.up, (Target.transform.position - transform.position).normalized, Time.deltaTime * 10);
		base.Attack();
	}
	#endregion
}
