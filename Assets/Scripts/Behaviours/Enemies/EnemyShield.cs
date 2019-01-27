/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;

public class EnemyShield : Enemy
{

	#region Variables
	int ShieldHealth;
	int ShieldMaxHealth;
	bool Regenerating;
	public GameObject ShieldObj;

	#endregion

	#region Unity Methods

	void Awake()
	{
		Speed = 0.2f;
		AttackDamage = 0;
		CoolDown = 0.25f;
		Range = 0.5f;
		Health = 20;
		MaxHealth = 20;
		ShieldHealth = 120;
		ShieldMaxHealth = 120;
		Regenerating = false;
	}

	new void Update()
	{
		base.Update();
		Shield();
	}

	protected new void LateUpdate()
	{
	}

	#endregion

	#region otherMethods
	void Shield()
	{

		if (Regenerating)
		{
			CurrentCoolDown += Time.deltaTime;
			if (CurrentCoolDown > CoolDown)
			{
				ShieldHealth++;
				CurrentCoolDown = 0;
				if (ShieldHealth == ShieldMaxHealth)
				{
					Regenerating = false;
					ShieldObj.SetActive(true);
				}
			}
			return;
		}
		foreach (Collider col in Physics.OverlapSphere(this.transform.position, this.Range / 2))
		{
			if (col.gameObject.tag == "Projectile")
			{
				if (ShieldHealth > 0 && col.gameObject.GetComponent<Projectile>().Origin is Satellite)
				{
					ShieldHealth -= col.gameObject.GetComponent<Projectile>().Origin.GetAttackDamage();
					Destroy(col.gameObject);
					CurrentCoolDown = 0;
					if (ShieldHealth <= 0)
					{
						ShieldHealth = 0;
						ShieldObj.SetActive(false);
						Regenerating = true;
						return;
					}
				}
			}
		}
	}
	#endregion
}
