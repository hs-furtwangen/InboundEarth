using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChanger : MonoBehaviour {
	public Material newMat; 

	void Start () {
		Invoke("ChangeMat", 0.5f);
	}
	
	void ChangeMat()
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			if(transform.GetChild(i).GetComponent<MeshRenderer>() != null)
			{
				transform.GetChild(i).GetComponent<MeshRenderer>().material = newMat;
			}
		}
	}
}
