using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRot : MonoBehaviour {
	public Transform target;

	void Update () {
		transform.rotation = target.rotation;		
	}
}
