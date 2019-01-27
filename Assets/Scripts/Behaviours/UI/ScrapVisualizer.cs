using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrapVisualizer : MonoBehaviour {
	public Text scrapCounter;

	float minScale = 0.2f;
	float maxScale = 0.9f;

	public Transform scrapSphere;

	void Update () {
		int scrapCurrent = GameState.GetScrap(); 
		scrapCounter.text = scrapCurrent.ToString();

		scrapSphere.localScale = Vector3.one * ClampedRemap(scrapCurrent, 0, 1000, minScale, maxScale);
		scrapSphere.Rotate(new Vector3(2f, 0.1f, -0.2f) * Time.deltaTime * 3f);
	}

	public float ClampedRemap (float _val, float _from1, float _to1, float _from2, float _to2) {
		_val = Mathf.Min(Mathf.Max(_val, _from1), _to1);
    	return (_val - _from1) / (_to1 - _from1) * (_to2 - _from2) + _from2;
	}
}
