/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;

public class SatelliteRepair : Satellite
{

	#region Variables
	private LineRenderer lr2;
	#endregion

	#region Unity Methods

	void Awake(){
		this.Health = 20;
		this.MaxHealth = 20;
		this.PreferredTarget = EPreferredTarget.CLOSEST;
		this.Range = 0.5f;
		this.CoolDown = 0.1f;
		this.AttackDamage = -1;
	}

	new void Start(){
		base.Start();
		lr2 = this.GetComponent<LineRenderer>();
	}
	
	new void Update () 
	{
		base.Update();
		if(!notRunningYet) Heal();
		if(!lr2) ReAddLR();
		if(Target){
			//TODO: Make this with particles instead.
			lr2.SetPosition(0,this.transform.position);
			lr2.SetPosition(1,Target.transform.position);
		} else {
			lr2.SetPosition(0,this.transform.position);
			lr2.SetPosition(1,this.transform.position);
		}
	}

	#endregion

	#region otherMethods

	void ReAddLR(){
		lr2 = this.gameObject.AddComponent<LineRenderer>();
		lr2.widthMultiplier = 0.01f;
		lr2.positionCount = 2;
		lr2.material = Resources.Load<Material>("Materials/HealingBeam");
	}
	void Heal(){
		if (CurrentCoolDown > CoolDown) {
			switch (this.PreferredTarget)
			{
				case EPreferredTarget.CLOSEST_TO_EARTH:
					this.Target = SatelliteManager.GetClosestToEarthInRange(this.transform, this.Range);
					break;
				case EPreferredTarget.LEAST_HEALTH:
					this.Target = SatelliteManager.GetLowestHealthInRange(this.transform, this.Range);
					break;
				case EPreferredTarget.MOST_HEALTH:
					this.Target = SatelliteManager.GetHighestHealthInRange(this.transform, this.Range);
					break;
				case EPreferredTarget.FURTHEST:
					this.Target = SatelliteManager.GetFurthestInRange(this.transform, this.Range);
					break;
				default:
					this.Target = SatelliteManager.GetClosestTo(this.transform, this.Range);
					break;
			}

			if(!Target) return;
			Target.Hit(this.AttackDamage);


			CurrentCoolDown = 0;
		}
	}
	#endregion
}
