using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemiesManager {
	static List<Enemy> enemyList = new List<Enemy>();

	public static void AddEnemy(Enemy enemy) {
		enemyList.Add(enemy);
	}
	public static void RemoveEnemy(Enemy enemy) {
		enemyList.Remove(enemy);
	}

	//Returns the enemy currently closest to earth WARNING: returns null if no enemies in list
	public static Enemy GetClosestToEarth() {
		if (enemyList.Count == 0) {
			return null;
		}
		Vector3 earth = GameState.GetEarth().transform.position;

		float minDist = Mathf.Infinity;
		Enemy closestEnemy = null;
		foreach (Enemy e in enemyList) {
			float cDist = Vector3.Distance(earth, e.transform.position);
			if (cDist < minDist) {
				minDist = cDist;
				closestEnemy = e;
			}
		}
		return closestEnemy;
	}

	public static Enemy GetClosestToEarthInRange(Transform t, float r = Mathf.Infinity){
		if (enemyList.Count == 0) {
			return null;
		}
		Vector3 earth = GameState.GetEarth().transform.position;

		float minDist = Mathf.Infinity;
		Enemy closestEnemy = null;
		foreach (Enemy e in enemyList) {
			float cDist = Vector3.Distance(t.position, e.transform.position);
			float eDist = Vector3.Distance(earth, e.transform.position);
			if (eDist < minDist && cDist < r) {
				minDist = cDist;
				closestEnemy = e;
			}
		}
		return closestEnemy;
	}

	//Returns closest enemy to a transform
	public static Enemy GetClosestTo(Transform t, float r = Mathf.Infinity) {
		if (enemyList.Count == 0) {
			return null;
		}

		float minDist = Mathf.Infinity;
		Enemy closestEnemy = null;
		foreach (Enemy e in enemyList) {
			float cDist = Vector3.Distance(t.position, e.transform.position);
			if (cDist < minDist && cDist < r) {
				minDist = cDist;
				closestEnemy = e;
			}
		}
		return closestEnemy;
	}

	public static Enemy GetLowestHealthInRange(Transform t, float r = Mathf.Infinity){
		if (enemyList.Count == 0) {
			return null;
		}
		
		int minHealth = int.MaxValue;
		Enemy closestEnemy = null;
		foreach (Enemy e in enemyList) {
			float cDist = Vector3.Distance(t.position, e.transform.position);
			if (e.getHealth() < minHealth && cDist < r) {
				minHealth = e.getHealth();
				closestEnemy = e;
			}
		}
		return closestEnemy;
	}

	public static Enemy GetHighestHealthInRange(Transform t, float r = Mathf.Infinity){
		if (enemyList.Count == 0) {
			return null;
		}
		
		int maxHealth = 0;
		Enemy closestEnemy = null;
		foreach (Enemy e in enemyList) {
			float cDist = Vector3.Distance(t.position, e.transform.position);
			if (e.getHealth() > maxHealth && cDist < r) {
				maxHealth = e.getHealth();
				closestEnemy = e;
			}
		}
		return closestEnemy;
	}

	public static Enemy GetFurthestInRange(Transform t, float r = Mathf.Infinity){
		if (enemyList.Count == 0) {
			return null;
		}
		
		float maxDist = -1;
		Enemy closestEnemy = null;
		foreach (Enemy e in enemyList) {
			float cDist = Vector3.Distance(t.position, e.transform.position);
			if (cDist > maxDist && cDist < r) {
				maxDist = cDist;
				closestEnemy = e;
			}
		}
		return closestEnemy;
	}

	public static List<Enemy> GetAllEnemiesInRange(Transform t, float r = Mathf.Infinity){
		List<Enemy> enemies = new List<Enemy>();

		foreach(Enemy e in enemyList){
			if (Vector3.Distance(t.position,e.transform.position) < r){
				enemies.Add(e);
			}
		}

		return enemies;
	}

}
