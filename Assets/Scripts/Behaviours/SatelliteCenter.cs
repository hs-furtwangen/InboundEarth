/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;

public class SatelliteCenter : MonoBehaviour
{

	#region Variables
	float Speed = 0;
	#endregion

	#region Unity Methods

	void Start () 
	{
		
	}
	
	void Update () 
	{
		this.transform.Rotate(Vector3.right, this.Speed * Time.deltaTime * 10);
		if(this.transform.childCount <= 0){
			Destroy(this.gameObject);
		}
	}

	#endregion

	#region otherMethods
	public void SetRotationAndSpeed(Vector3 satellitePosition, Vector3 movementPosition){
		// this.transform.rotation = Quaternion.Euler(0, rotation, 0);
		// this.Speed = speed;

		Vector3 newUp = satellitePosition - this.transform.position;
		Vector3 gesture = movementPosition - satellitePosition;

		this.transform.rotation = Quaternion.LookRotation(gesture, newUp);
		this.Speed = calculateSpeed(satellitePosition, movementPosition);
	}

	public static float calculateSpeed(Vector3 satellitePosition, Vector3 movementPosition){
		float speed = 0;
		Vector3 gesture = movementPosition - satellitePosition;
		speed = Vector3.Magnitude(gesture * 10);
		speed = Mathf.Clamp(Mathf.Round(speed),0,10);
		return speed;
	}
	#endregion
}
