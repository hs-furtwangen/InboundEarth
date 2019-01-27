/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class MenuSelector : MonoBehaviour
{
	public LineRenderer lr;
	public GameObject controller;
	public LayerMask lm;
	public EarthMenuScript earth;
	private bool FadeOut = false;
	CanvasGroup cg;

	void Start()
	{
		cg = GetComponent<CanvasGroup>();
	}

	void Update()
	{
		if(FadeOut){
			cg.alpha -= Time.deltaTime / 2;
			lr.SetPosition(0, new Vector3(0,0,0));
			lr.SetPosition(1, new Vector3(0,0,0));
			return;
		}
		cg.alpha = Mathf.Clamp(cg.alpha += Time.deltaTime / 2,0,1);
		RaycastHit rh;
		lr.SetPosition(0, controller.transform.position);
		if (Physics.Raycast(controller.transform.position, controller.transform.forward, out rh, 5f, lm))
		{
			lr.SetPosition(1, rh.point);
			if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
			{
				switch (rh.transform.gameObject.name)
				{
					case "Back2Creds":
						earth.goTo("MainCredits");
						break;
					case "Back2Main":
						earth.goTo("MainMenu");
						break;
					case "Back2Game":
						earth.goTo("MainGame");
						break;
				}
				FadeOut = true;
			}
		}
		else
		{
			lr.SetPosition(1, controller.transform.position + controller.transform.forward * 5);
		}

	}
}
