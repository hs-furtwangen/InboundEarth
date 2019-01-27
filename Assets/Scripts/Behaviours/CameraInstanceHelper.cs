using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInstanceHelper : MonoBehaviour {

	public static CameraInstanceHelper instance;

	// Use this for initialization
	void Start () {
		if(instance == null)
		{
			instance = this;
		}
		else {
			Destroy(this);
		}
	}
}
