/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;

public class FinalBoss : Enemy
{

	#region Variables

	#endregion

	#region Unity Methods

	void Awake()
	{
		Speed = 0.08f;
		AttackDamage = 100000000;
		CoolDown = 80f;
		Range = 0.1f;
		Health = 15000;
		MaxHealth = 15000;
		Target = GameState.GetEarth();
	}

	new void Update()
	{
		Target = GameState.GetEarth();
		Move();
	}

	#endregion

	#region otherMethods

	public new void Move()
	{
		if (!InRange())
		{
			transform.LookAt(Target.transform);
			transform.Translate(Vector3.forward * Speed * Time.deltaTime);
		}
		else
		{
			Attack();
		}
	}
	#endregion
}
