/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;
using Valve.VR;
using UnityEngine.XR;

public class InputManager : MonoBehaviour
{

	#region Variables
	public GameObject SatellitePrefab;
	private int placingState = 0;
	private GameObject lastSatellite;
	#endregion

	#region Unity Methods

	void Start()
	{

	}

	void Update()
	{
		// if (SteamVR_Input._default.inActions.Teleport.GetStateDown(SteamVR_Input_Sources.Any))
		// {
		// 	interactSatellite(InputTracking.GetLocalPosition(XRNode.RightHand));
		// }
	}

	#endregion

	#region otherMethods
	void interactSatellite(Vector3 inputPosition)
	{
		if(Vector3.Magnitude(GameState.GetEarth().transform.position - inputPosition) <= 0.25) return;
		switch (placingState)
		{
			case 0:
				lastSatellite = Instantiate(SatellitePrefab);
				lastSatellite.transform.position = inputPosition;
				placingState++;
				return;
			case 1:
				lastSatellite.GetComponent<Satellite>().SetMovement(inputPosition);
				placingState = 0;
				break;
		}
	}
	#endregion
}
