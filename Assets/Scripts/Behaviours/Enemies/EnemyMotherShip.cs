using UnityEngine;

public class EnemyMotherShip : Enemy {

	EnemyKamikaze KamikazeShip;
	float SpawnCoolDown;
	float CurrentSpawnCoolDown;
	float CoolDownLaser = 1f;
	float CurrentCoolDownLaser = 0.5f;


	float RangeLaser = 0.7f;

	void Awake () {
		Speed = 0.3f;
		AttackDamage = 12;
		CoolDown = 0.25f;
		Range = 1.5f;
		Health = 200;
		MaxHealth = 200;
		Target = GameState.GetEarth();
		FixedTarget = true;
		KamikazeShip = Resources.Load<EnemyKamikaze>("Prefabs/Enemies/EnemyKamikaze");
	}
	
	public override void Attack() {
		//Still move when not in laser range bust slow down. Dirty!
		float dist = Vector3.Distance(transform.position, GameState.GetEarth().transform.position);
		if (dist > RangeLaser) {
			transform.Translate(Vector3.forward * Speed * 0.5f * Time.deltaTime);
		}
		if (CurrentCoolDown > CoolDown) {
			EnemyKamikaze km = Instantiate(KamikazeShip, transform.GetChild(0).transform.position, transform.rotation);
			CurrentCoolDown = 0;
		}
		CurrentCoolDownLaser += Time.deltaTime;
		if (CurrentCoolDownLaser > CoolDownLaser && dist < RangeLaser) {
			Projectile pr = Instantiate(projectile, this.transform.position, Quaternion.identity);
			pr.Target = GameState.GetEarth();
			pr.Origin = this.GetComponent<AttackingObject>();

			CurrentCoolDownLaser = 0;
		}
	}
}
