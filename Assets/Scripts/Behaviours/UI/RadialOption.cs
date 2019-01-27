using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialOption : MonoBehaviour {
	public Image radialPie;
	public Image icon;
	public Text cost; // If applicable
	public Color baseColor;
	public bool available;

	// Use this for initialization
	void Start () {
		// radialPie = GetComponentsInChildren<Image>()[0];
		// icon = GetComponentsInChildren<Image>()[1];
		// cost = GetComponentsInChildren<Text>()[0];		
	}
	
	// Update is called once per frame
	void Update () {
		Color newColor = Color.white;
		if(available)
		{
			newColor = Color.Lerp(radialPie.color, baseColor, 0.5f * Time.deltaTime);
		} else {
			newColor = Color.Lerp(radialPie.color, Color.red, 0.5f * Time.deltaTime);
		}
		radialPie.color = newColor;
	}

	public void UpdateContent(Sprite _sprite, string _text)
	{
		icon.sprite = _sprite;
		cost.text = _text;
	}
}
