using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using UnityEngine.XR;

/*
Something copyright (c) something Exaii and Plagiatus
*/

public class RadialMenuScript : MonoBehaviour {

	public RadialOption[] buttons; // first child gets scaled
	public int currentButton;
	public Satellite currentSatellite;
	public float maxScaleActive = 1.2f;
	public Vector2 input;
	public float angleCurrent;

	public ERadialState state;
	public float minMagnitude = 0.04f;
	public float satelliteSelectionRange = 0.25f;

	public Text satelliteDistance;

	public Transform hideWhileBuildingActive;
	public Transform showWhileBuildingActive;

	public MeshRenderer scrapOrbRef;
	public Material scrapMat;

	public GameObject[] SatellitePrefabs;

	void Start () {
		GetButtons();
		SetButtonRot();	
		scrapMat = scrapOrbRef.materials[0];
	}
	
	void Update () {
		transform.parent.rotation = CameraInstanceHelper.instance.transform.rotation;

		// Choosing menu type, based on context
		if(state == ERadialState.none) {
			currentSatellite = SatelliteManager.GetClosestTo(this.transform, satelliteSelectionRange);
			Time.timeScale = 1f;
		}

		if(state == ERadialState.none && SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any) && currentSatellite != null)
		{
			state = ERadialState.attackState;
			UpdateContentAttack();
			Time.timeScale = 0.005f;
		} else if((state == ERadialState.none || state == ERadialState.buildStateChoose1)  && SteamVR_Input._default.inActions.Teleport.GetStateDown(SteamVR_Input_Sources.Any)) {
			state = ERadialState.buildState;
			UpdateContentBuild();
			CheckRadialOptionAvailability();
		}

		// General radial menu stuff
		if(state == ERadialState.attackState || state == ERadialState.buildState) // && SteamVR_Input._default.inActions.Teleport.GetState(SteamVR_Input_Sources.Any)
		{
			input = SteamVR_Input._default.inActions.TouchPadTouch.GetAxis(SteamVR_Input_Sources.Any);
			//input.Normalize();
			if(input.sqrMagnitude > minMagnitude)
			{
				currentButton = GetNearestButton(input);
			} else {
				Debug.Log("Not far away from the center enough");
				currentButton = -1;
			}
			scrapMat.SetFloat("Vector1_FCED63AB", Mathf.Lerp(scrapMat.GetFloat("Vector1_FCED63AB"), 1f, Time.deltaTime));
			transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 6 * Time.deltaTime);
			hideWhileBuildingActive.localScale = Vector3.Lerp(hideWhileBuildingActive.localScale, Vector3.one, 6 * Time.deltaTime);
			showWhileBuildingActive.localScale = Vector3.Lerp(showWhileBuildingActive.localScale, Vector3.zero, 6 * Time.deltaTime);
		} else if(state == ERadialState.buildStateChoose1 || state == ERadialState.buildStateChoose2) { // state == ERadialState.buildStateChoose1 || 
			if(state == ERadialState.buildStateChoose1)
			{
				transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 6 * Time.deltaTime);
				hideWhileBuildingActive.localScale = Vector3.Lerp(hideWhileBuildingActive.localScale, Vector3.one, 6 * Time.deltaTime);
				showWhileBuildingActive.localScale = Vector3.Lerp(showWhileBuildingActive.localScale, Vector3.zero, 6 * Time.deltaTime);
				scrapMat.SetFloat("Vector1_FCED63AB", Mathf.Lerp(scrapMat.GetFloat("Vector1_FCED63AB"), 1.5f, Time.deltaTime));
			} else {
				if(currentButton != -1 && currentButton != 3)
				{
					satelliteDistance.text = currentSatellite.GetDistance(transform.position);
				}
				transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 6 * Time.deltaTime);
				hideWhileBuildingActive.localScale = Vector3.Lerp(hideWhileBuildingActive.localScale, Vector3.zero, 6 * Time.deltaTime);
				showWhileBuildingActive.localScale = Vector3.Lerp(showWhileBuildingActive.localScale, Vector3.one, 6 * Time.deltaTime);
				scrapMat.SetFloat("Vector1_FCED63AB", Mathf.Lerp(scrapMat.GetFloat("Vector1_FCED63AB"), 2f, Time.deltaTime));
			}
		} else {
			transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 6 * Time.deltaTime);
			hideWhileBuildingActive.localScale = Vector3.Lerp(hideWhileBuildingActive.localScale, Vector3.one, 6 * Time.deltaTime);
			showWhileBuildingActive.localScale = Vector3.Lerp(showWhileBuildingActive.localScale, Vector3.zero, 6 * Time.deltaTime);
			scrapMat.SetFloat("Vector1_FCED63AB", Mathf.Lerp(scrapMat.GetFloat("Vector1_FCED63AB"), 0f, Time.deltaTime));
		}

		// Upon choosing from radial type
		if(currentButton == -1 && 
			(state == ERadialState.buildState && 
			SteamVR_Input._default.inActions.Teleport.GetStateUp(SteamVR_Input_Sources.Any) || 
			state == ERadialState.attackState && 
			SteamVR_Input._default.inActions.GrabPinch.GetStateUp(SteamVR_Input_Sources.Any)))
		{
			state = ERadialState.none;	
		}
		else if(state == ERadialState.buildState && SteamVR_Input._default.inActions.Teleport.GetStateUp(SteamVR_Input_Sources.Any)) {
			ExecuteContentBuild();
			Time.timeScale = 1f;
		//} else if(state == ERadialState.attackState && SteamVR_Input._default.inActions.GrabPinch.GetStateUp(SteamVR_Input_Sources.Any)) {
		} else if(state == ERadialState.attackState && SteamVR_Input._default.inActions.Teleport.GetStateUp(SteamVR_Input_Sources.Any)) {
			Debug.Log("Finished changing attack");
			ExecuteContentAttack(currentSatellite);
			currentButton = -1;
			Time.timeScale = 1f;
			state = ERadialState.none;
		}

		// When having already chosen a built option
		if(currentButton != -1 && currentButton != 3) // In order to prevent a null ref
		{
			if(
				state == ERadialState.buildStateChoose1 &&
				SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any) &&
				!SteamVR_Input._default.inActions.Teleport.GetState(SteamVR_Input_Sources.Any) &&
				GameState.SpendScrap(GetScrapValue(currentButton))
				)
			{
				Debug.Log("Placing first pos for sat");
				InstantiateSatellite(currentButton);
				state = ERadialState.buildStateChoose2;
			} else if(
				state == ERadialState.buildStateChoose2 && 
				SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any)
				)
			{
				Debug.Log("Placing sec pos for sat");
				MoveSatellite();
				state = ERadialState.none;
				currentButton = -1;
			}
		}

		RecalcButtons();
	}

	private int GetNearestButton(Vector2 _input)
	{
		float angle = angleCurrent = Vector2.SignedAngle(Vector2.up, _input);  //Mathf.Tan(Mathf.Deg2Rad * _input.x / _input.y);
		if(angle < 0)
		{
			angle = angleCurrent += 360;
		}
		angle = angleCurrent = 360 - angle;

		float optionAngleRange = 360 / buttons.Length;
		int currentOption = -1;

		// for(int i = 0; i < buttons.Length; i++)
		// {
		// 	float deltaAngle = Mathf.DeltaAngle(angle, buttons[i].GetComponent<RectTransform>().rotation.eulerAngles.z);
		// 	if(deltaAngle < optionAngleRange / 2) {
		// 		currentOption = i;
		// 	}	
		// }

		angle = (angle) / optionAngleRange;
		currentOption = Mathf.RoundToInt(angle);
		if(currentOption > buttons.Length - 1) 
			currentOption = 0;
		

		if(!buttons[currentOption].available && currentOption != 3 && state == ERadialState.buildState)
		{
			currentOption = -1;
		}

		return currentOption;
	}

	public void UpdateContent()
	{
		switch(state) {
			case ERadialState.attackState:
				CheckRadialOptionAvailability();
				break;
			case ERadialState.buildState:
				CheckRadialOptionAvailability();
				break;
			case ERadialState.none:
			case ERadialState.buildStateChoose1:
			default:
				break;
		}
	}

	public void UpdateContentAttack()
	{
		buttons[0].UpdateContent(null, "Most health");
		buttons[1].UpdateContent(null, "Closest to Earth");
		buttons[2].UpdateContent(null, "Closest");
		buttons[3].UpdateContent(null, "CANCEL");
		buttons[4].UpdateContent(null, "Furthest");
		buttons[5].UpdateContent(null, "Least health");
	}
	public void ExecuteContentAttack(Satellite _target)
	{
		switch(currentButton) {
			case 3:
				Debug.Log("Cancel");
//				Time.timeScale = 1;
//				state = ERadialState.none;
				break;
			case 0:
				_target.PreferredTarget = EPreferredTarget.MOST_HEALTH;
//				Time.timeScale = 1;
//				state = ERadialState.none;
				break;
			case 1:
				_target.PreferredTarget = EPreferredTarget.CLOSEST_TO_EARTH;
//				Time.timeScale = 1;
//				state = ERadialState.none;
				break;
			case 2: // is the option right of cancel
				_target.PreferredTarget = EPreferredTarget.CLOSEST;
//				Time.timeScale = 1;
//				state = ERadialState.none;
				break;
			case 4: // is the option left of cancel
				_target.PreferredTarget = EPreferredTarget.FURTHEST;
//				Time.timeScale = 1;
//				state = ERadialState.none;
				break;
			case 5:
				_target.PreferredTarget = EPreferredTarget.LEAST_HEALTH;
//				Time.timeScale = 1;
//				state = ERadialState.none;
				break;
			case 6:
				Debug.Log("The forbidden button. So it has come to this.");
//				Time.timeScale = 1;
//				state = ERadialState.none;
				break;
			default:
				state = ERadialState.none;
				break;
		}
	}
	public void UpdateContentBuild()
	{
		buttons[0].UpdateContent(null, "Rep");
		buttons[1].UpdateContent(null, "Mag");
		buttons[2].UpdateContent(null, "Shield");
		buttons[3].UpdateContent(null, "CANCEL");
		buttons[4].UpdateContent(null, "Rocks");
		buttons[5].UpdateContent(null, "Lazer");
	}
	public void ExecuteContentBuild()
	{
		switch(currentButton) {
			case 3:
				Debug.Log("Cancel");
				state = ERadialState.none;
				break;
			case 0: // Rep
				if(GameState.TrySpendScrap(GetScrapValue(0))) {
					state = ERadialState.buildStateChoose1;
				}
				break;
			case 1: // Mag
				if(GameState.TrySpendScrap(GetScrapValue(1))) {
					state = ERadialState.buildStateChoose1;
				}
				break;
			case 2: // is the option right of cancel
				if(GameState.TrySpendScrap(GetScrapValue(2))) {
					state = ERadialState.buildStateChoose1;
				}
				break;
			case 4: // is the option left of cancel
				if(GameState.TrySpendScrap(GetScrapValue(4))) {
					state = ERadialState.buildStateChoose1;
				}
				break;
			case 5:
				if(GameState.TrySpendScrap(GetScrapValue(5))) {
					state = ERadialState.buildStateChoose1;
				}
				break;
			case 6:
				Debug.Log("The forbidden button. So it has come to this.");
				state = ERadialState.none;
				break;
			default:
				state = ERadialState.none;
				break;
		}
	}

	public int GetScrapValue(int _target)
	{
		if(state == ERadialState.buildState || state == ERadialState.buildStateChoose1 || state == ERadialState.buildStateChoose2 || state == ERadialState.none)
		{
			Satellite sat = SatellitePrefabs[_target].GetComponent<Satellite>();
			if(_target == 0)
			{
				return 50;
			} else if(_target == 1)
			{
				return 200;
			} else if(_target == 2)
			{
				return 80;
			} else if(_target == 4)
			{
				return 30;
			} else if(_target == 5)
			{
				return 50;
			}
		}
		return 0;
	}

	private void CheckRadialOptionAvailability()
	{
		if(state == ERadialState.attackState)
		{
			for(int i = 0; i < buttons.Length; i++)
			{
				buttons[i].available = true;
			}
		} else if(state == ERadialState.buildState)
		{
			for(int i = 0; i < buttons.Length; i++)
			{
				if(i != 3) 
				{
					if(GameState.TrySpendScrap(GetScrapValue(i)))
					{
						buttons[i].available = true;
					} else {
						buttons[i].available = false;
					}
				} else {
						buttons[i].available = true;
				}
			}
		}
	}

	private void InstantiateSatellite(int _target)
	{
		Debug.Log(_target);
		GameObject newSatellite = Instantiate(SatellitePrefabs[_target], this.transform.position, Quaternion.identity);
		currentSatellite = newSatellite.GetComponent<Satellite>();
	}

	private void MoveSatellite()
	{
		currentSatellite.SetMovement(this.transform.position);
		//currentSatellite = null;
	}

	private void RecalcButtons()
	{
		for(int i = 0; i < buttons.Length; i++)
		{
			if(i == currentButton) {
				RecalculateButtonScale(i, true);
			} else {
				RecalculateButtonScale(i, false);
			}
		}
	}

	private void OnClick()
	{

	}

	private void GetButtons()
	{
		int childs = transform.childCount;
		buttons = new RadialOption[childs];
		for(int i = 0; i < childs; i++)
		{
			buttons[i] = transform.GetChild(i).GetComponent<RadialOption>();
		}
	}

	private void SetButtonRot()
	{
		// TODO: Calling this sets the rotation value of the gameobjects.
		// Maybe some day.
	}

	private void RecalculateButtonScale(int _target, bool _active)
	{
		if(_active) {
			buttons[_target].transform.localScale = Vector3.Lerp(buttons[_target].transform.localScale, new Vector3(maxScaleActive, maxScaleActive, maxScaleActive), 2f * Time.deltaTime);
		} else {
			buttons[_target].transform.localScale = Vector3.Lerp(buttons[_target].transform.localScale, new Vector3(1f, 1f, 1f), 2f * Time.deltaTime);
		}
	}
}

public enum ERadialState {
	none,
	attackState, // opens radial menu until chosen (or option 2: X)
	buildState, // opens radial menu until chosen (or option 2: X)
	buildStateChoose1, // after choosing, this will be the current state. Ends when placing first position of satellite
	buildStateChoose2, // after choosing, this will be the current state. Ends when placing last position of satellite
}
