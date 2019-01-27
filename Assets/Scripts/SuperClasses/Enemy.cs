/*
*		Script by Plagiatus
*		No Steal plz. (Also, Copyright, obviously).
*/

using UnityEngine;

public abstract class Enemy : AttackingObject
{
    #region Variables

    #endregion

    #region Unity Methods
    Vector3 randomDirection;
    protected bool FixedTarget = false;
    void Start()
    {
        EnemiesManager.AddEnemy(this);
        if (!FixedTarget) { //Asteroids, Kamikaze etc.
            SelectTarget();
            InvokeRepeating("SelectTarget", 1, 1);
        }
    }

    void Awake() {
        randomDirection = new Vector3(Random.Range(-0.25f,-0.25f),Random.Range(-0.25f,-0.25f),Random.Range(-0.15f,-0.15f));
    }

    void OnDestroy()
    {
        //Death anim. Sound etc
        //Watch out for 0ref
        EnemiesManager.RemoveEnemy(this);
    }

    protected virtual void Update()
    {
        if (Target == null) {
            SelectTarget();
        }
        Move();
    }

    protected void SelectTarget()
    {

        Satellite st = SatelliteManager.GetClosestTo(this.transform, this.Range);
        //ugly af
        if (Random.Range(0, 10) < 8) {
            if (st != null) {
                Target = st;
            } else {
            Target = GameState.GetEarth();
        }
        } else {
            Target = GameState.GetEarth();
        }
    }

    protected void SelectTargetCustomRange(float CustomRange)
    {
        //ugly af
        if (Random.Range(0, 10) < 8) {
            Satellite st = SatelliteManager.GetClosestTo(this.transform, CustomRange);
            if (st != null) {
                Target = st;
            } else {
            Target = GameState.GetEarth();
        }
        } else {
            Target = GameState.GetEarth();
        }
    }


    protected bool InRange()
    {
        float distance = Vector3.Distance(transform.position, Target.transform.position);

        //Check if earth is target to respect earth radius
        distance -= Target.GetComponent<Earth>() ? Earth.radius : 0;

        return distance > Range && Target.GetComponent<Earth>() ? false : true;
    }
    protected bool InCustomRange(float r)
    {
        float distance = Vector3.Distance(transform.position, Target.transform.position);

        //Check if earth is target to respect earth radius
        distance -= Target.GetComponent<Earth>() ? Earth.radius : 0;

        return distance > r && Target.GetComponent<Earth>() ? false : true;
    }

    public virtual new void Move()
    {
        if (!InRange())
        {
            transform.LookAt(Target.transform);
            transform.Translate((Vector3.forward + randomDirection) * Speed * Time.deltaTime);
        }
        else
        {
            transform.LookAt(Target.transform);
            Attack();
        }
    }

	#endregion

	#region otherMethods

	#endregion
}
