/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;
using UnityEngine.XR;

public class SpaceDebris : MonoBehaviour
{

	#region Variables
	Vector3 MovementDirection = Vector3.zero;
	int Value;
	#endregion

	#region Unity Methods

	void Start()
	{
		MovementDirection = new Vector3(Random.Range(-0.1f,0.1f), Random.Range(-0.1f,0.1f), Random.Range(-0.1f,0.1f));
	}

	void Update()
	{
		this.transform.Translate(MovementDirection * Time.deltaTime);
		this.transform.Rotate(MovementDirection * 10);
		if(Vector3.SqrMagnitude(this.transform.position) > 10){
			Destroy(this.gameObject);
		}

		if(Vector3.SqrMagnitude(this.transform.position - InputTracking.GetLocalPosition(XRNode.RightHand)) < 0.0025f){
			GameState.AddScrap(Value);
			Destroy(this.gameObject);
		}
	}

	#endregion

	#region otherMethods

	public static void CreateNew(Vector3 pos, int value)
	{
		int r = Random.Range(1,6);
		string path = string.Format("Prefabs/SpaceDebris/Scrap0{0}", r); //adjust if > 09 parts
		GameObject instantiateThis = Resources.Load<GameObject>(path);
		
		while (value > 0)
		{
			GameObject newInst = Instantiate(instantiateThis);
			newInst.transform.position = pos;
			newInst.GetComponent<SpaceDebris>().Value = 10;
			if (value < 20){
				newInst.GetComponent<SpaceDebris>().Value = value / 2;
			}
			value -= 20;
		}
	}

	#endregion
}
