/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class Satellite : AttackingObject
{

	#region Variables
	public EPreferredTarget PreferredTarget;
	protected GameObject Earth;
	protected GameObject SatelliteCenterObj;
	protected SatelliteCenter SatelliteCenterScript;
	public bool notRunningYet = true;
	private LineRenderer lr;
	//private Text distanceText;
	private SphereCollider sphCollider;
	#endregion

	#region Unity Methods

	public void Start()
	{
		sphCollider = this.GetComponent<SphereCollider>();
		// distanceText = GameObject.Find("SpeedText").GetComponent<Text>();
		lr = this.GetComponent<LineRenderer>();

		Earth = GameObject.FindGameObjectWithTag("Earth");

		SatelliteCenterObj = new GameObject();
		SatelliteCenterObj.transform.position = Earth.transform.position;
		SatelliteCenterObj.name = "SatelliteCenter";
		//SetMovement(transform.position); //FÜR VR WIEDER ENTFERNEN SPÄTER! TODO
	}

	public void Update()
	{
		if (notRunningYet) DrawLine();
		Move();
	}

	void FixedUpdate()
	{
		if (notRunningYet) return;
		foreach (Collider collider in Physics.OverlapSphere(this.transform.position, sphCollider.radius))
		{
			if (collider.gameObject != this.gameObject)
			{
				// Debug.Log("Collision with " + raycastHit.collider.gameObject.name);
				Satellite sat = collider.gameObject.GetComponent<Satellite>();
				if (sat && !sat.notRunningYet)
				{
					sat.Hit(1);
				}
			}
		}
	}

	void OnDestroy()
	{
		SatelliteManager.RemoveSatellite(this);
	}

	#endregion

	#region otherMethods
	// override public void Move()
	// {
	// 	// this.transform.LookAt(Earth.transform.position);
	// 	// rb.AddRelativeForce(Vector3.right * Time.deltaTime * this.Speed);
	// 	// this.transform.Rotate(Vector3.forward, this.Speed * Time.deltaTime * 10);
	// }

	public void SetMovement(Vector3 controllerPos)
	{
		SatelliteCenterScript = SatelliteCenterObj.AddComponent<SatelliteCenter>();
		SatelliteCenterScript.SetRotationAndSpeed(this.transform.position, controllerPos);
		this.transform.SetParent(SatelliteCenterObj.transform);
		notRunningYet = false;
		Destroy(lr);
		SatelliteManager.AddSatellite(this);
		//distanceText.text = "";
	}

	virtual public void Upgrade()
	{

	}

	void DrawLine()
	{
		float distance = Vector3.Magnitude(this.transform.position - Earth.transform.position);
		Vector3 controllerPos = InputTracking.GetLocalPosition(XRNode.RightHand);

		lr.positionCount = 11;
		lr.SetPosition(0, this.transform.position);
		Vector3 correctDistanceEndPoint = controllerPos - Earth.transform.position;
		correctDistanceEndPoint = Earth.transform.position + (correctDistanceEndPoint.normalized * distance);
		Vector3 correctDistancePoint = new Vector3();

		for (int i = 1; i < 10; i++)
		{
			float multiplier = i / 10f;
			correctDistancePoint = multiplier * (correctDistanceEndPoint - this.transform.position) + this.transform.position;
			correctDistancePoint -= Earth.transform.position;
			correctDistancePoint = Earth.transform.position + (correctDistancePoint.normalized * distance);
			lr.SetPosition(i, correctDistancePoint);
		}
		lr.SetPosition(10, correctDistanceEndPoint);

		//distanceText.text = SatelliteCenter.calculateSpeed(this.transform.position, controllerPos).ToString();
	}

	public string GetDistance(Vector3 _pos)
	{
		return SatelliteCenter.calculateSpeed(this.transform.position, _pos).ToString();
	}

	protected void ChooseTarget()
	{
		if(notRunningYet) return;
		switch (this.PreferredTarget)
		{
			case EPreferredTarget.CLOSEST_TO_EARTH:
				this.Target = EnemiesManager.GetClosestToEarthInRange(this.transform, this.Range);
				break;
			case EPreferredTarget.LEAST_HEALTH:
				this.Target = EnemiesManager.GetLowestHealthInRange(this.transform, this.Range);
				break;
			case EPreferredTarget.MOST_HEALTH:
				this.Target = EnemiesManager.GetHighestHealthInRange(this.transform, this.Range);
				break;
			case EPreferredTarget.FURTHEST:
				this.Target = EnemiesManager.GetFurthestInRange(this.transform, this.Range);
				break;
			default:
				this.Target = EnemiesManager.GetClosestTo(this.transform, this.Range);
				break;
		}
	}


	#endregion
}
