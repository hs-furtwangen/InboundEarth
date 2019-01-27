/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;
using System.Collections.Generic;

public class SatelliteMagnet : Satellite
{

	#region Variables
	float PullMultiplier = 0.1f;

	#endregion

	#region Unity Methods

	void Awake()
	{
		this.Health = 500;
		this.MaxHealth = 500;
		this.PreferredTarget = EPreferredTarget.CLOSEST;
		this.Range = 0.5f;
		this.CoolDown = 0.1f;
		this.AttackDamage = 1;
	}

	new void Update()
	{
		base.Update();
		Attack();
	}

	#endregion

	#region otherMethods

	new void Attack()
	{
		List<Enemy> enemies = EnemiesManager.GetAllEnemiesInRange(this.transform, this.Range);
		foreach (Enemy e in enemies)
		{
			float dist = Vector3.Distance(this.transform.position, e.transform.position);
			e.transform.position = Vector3.MoveTowards(e.transform.position, this.transform.position, Time.deltaTime * PullMultiplier * (5 - dist * (5 * (1 / this.Range))));
			if (CurrentCoolDown > CoolDown)
			{
				e.Hit(
					Mathf.RoundToInt(5 - dist * (4 * (1 / this.Range)))
				);
				CurrentCoolDown = 0;
			}
		}
	}

	#endregion
}
