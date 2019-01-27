using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SatelliteManager{
	static List<Satellite> satelliteList = new List<Satellite>();

	public static void AddSatellite(Satellite s){
		satelliteList.Add(s);
	}

	public static void RemoveSatellite(Satellite s){
		satelliteList.Remove(s);
	}

	public static Satellite GetClosestTo(Transform t, float r = Mathf.Infinity){
		if (satelliteList.Count == 0){
			return null;
		}

		float minDist = Mathf.Infinity;
		Satellite closestSatellite = null;
		foreach (Satellite s in satelliteList) {
			float cDist = Vector3.Distance(t.position, s.transform.position);
			if (cDist < minDist && cDist < r && s.transform != t) {
				minDist = cDist;
				closestSatellite = s;
			}
		}
		return closestSatellite;
	}

	public static Satellite GetLowestHealthInRange(Transform t, float r = Mathf.Infinity){
		if (satelliteList.Count == 0) {
			return null;
		}
		
		int minHealth = int.MaxValue;
		Satellite closestSatellite = null;
		foreach (Satellite s in satelliteList) {
			float cDist = Vector3.Distance(t.position, s.transform.position);
			if (s.getHealth() < minHealth && cDist < r && s.transform != t) {
				minHealth = s.getHealth();
				closestSatellite = s;
			}
		}
		return closestSatellite;
	}

	public static Satellite GetHighestHealthInRange(Transform t, float r = Mathf.Infinity){
		if (satelliteList.Count == 0) {
			return null;
		}
		
		int maxHealth = 0;
		Satellite closestSatellite = null;
		foreach (Satellite s in satelliteList) {
			float cDist = Vector3.Distance(t.position, s.transform.position);
			if (s.getHealth() > maxHealth && cDist < r && s.transform != t) {
				maxHealth = s.getHealth();
				closestSatellite = s;
			}
		}
		return closestSatellite;
	}

	public static Satellite GetClosestToEarthInRange(Transform t, float r = Mathf.Infinity){
		if (satelliteList.Count == 0) {
			return null;
		}
		Vector3 earth = GameState.GetEarth().transform.position;

		float minDist = Mathf.Infinity;
		Satellite closestSatellite = null;
		foreach (Satellite s in satelliteList) {
			float cDist = Vector3.Distance(t.position, s.transform.position);
			float eDist = Vector3.Distance(earth, s.transform.position);
			if (eDist < minDist && cDist < r && s.transform != t) {
				minDist = cDist;
				closestSatellite = s;
			}
		}
		return closestSatellite;
	}

	public static Satellite GetFurthestInRange(Transform t, float r = Mathf.Infinity){
		if (satelliteList.Count == 0){
			return null;
		}

		float maxDist = -1;
		Satellite closestSatellite = null;
		foreach (Satellite s in satelliteList) {
			float cDist = Vector3.Distance(t.position, s.transform.position);
			if (cDist > maxDist && cDist < r && s.transform != t) {
				maxDist = cDist;
				closestSatellite = s;
			}
		}
		return closestSatellite;
	}

}
