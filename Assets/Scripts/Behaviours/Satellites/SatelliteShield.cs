/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;

public class SatelliteShield : Satellite
{

	#region Variables
	public GameObject ShieldObj;
	public int ShieldHealth;
	float ShieldMaxHealth;
	float RegenCoolDown;
	float CurrentRegenCoolDown;
	bool Regenerating;
	#endregion

	#region Unity Methods

	void Awake()
	{
		this.Health = 60;
		this.MaxHealth = 60;
		this.PreferredTarget = EPreferredTarget.CLOSEST;
		this.Range = 0.4f;
		this.AttackDamage = 0;
		this.CoolDown = 5;
		this.ShieldHealth = 200;
		this.ShieldMaxHealth = 200;
		this.Regenerating = false;
		this.CurrentRegenCoolDown = 0;
		this.RegenCoolDown = 0.05f;
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
		
		if(Regenerating){
			CurrentRegenCoolDown += Time.deltaTime;
			if(CurrentRegenCoolDown > RegenCoolDown){
				ShieldHealth++;
				CurrentRegenCoolDown = 0;
				if(ShieldHealth == ShieldMaxHealth){
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
				if (ShieldHealth > 0 && col.gameObject.GetComponent<Projectile>().Origin is Enemy)
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
