using UnityEngine;

public class magnetRotation : MonoBehaviour {
	public float RotSpeed = 30;
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * RotSpeed * Time.deltaTime, Space.Self);
	}
}
