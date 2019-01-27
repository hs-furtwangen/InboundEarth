/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;


public class SoundManager : MonoBehaviour
{
	// public static SoundManager Instance;
	public AudioClip[] Explosion;
	public AudioClip[] LaserSound;
	public AudioClip DebrisPickup;
	public AudioClip MissleLaunch;

	void Start(){
		// this.Instance = this;
	}

	public void PlaySomething(string typeOfSound, Vector3 position){
		switch(typeOfSound){
			case "Explosion":
				AudioSource.PlayClipAtPoint(Explosion[Random.Range(0,Explosion.Length)],position);
			break;
			case "LaserSound":
				AudioSource.PlayClipAtPoint(LaserSound[Random.Range(0,LaserSound.Length)],position);
			break;
			case "Debris":
				AudioSource.PlayClipAtPoint(DebrisPickup,position);
			break;
			case "Missle":
				AudioSource.PlayClipAtPoint(MissleLaunch,position);
			break;
		}
	}
}
