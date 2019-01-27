using UnityEngine;

public class Explosion : MonoBehaviour {

	float lifetime = 1;

	void LateUpdate () {
		lifetime -= Time.deltaTime;
		if (lifetime < 0) {
			Destroy(this.gameObject);
		}
	}
}
