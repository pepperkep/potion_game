using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMovement : Movement
{
    [SerializeField] private Transform target;
    [SerializeField] private float turnTowardsTargetRange = 4f;
    [SerializeField] private float turnSpeed = 0.1f;
    private bool isTurning = false;
    private float rotatePoint = 0;
    
    public override Transform Target
    {
        get => target;
        set => target = value;
    }

    private Rigidbody2D enemybody;

    void Start()
    {
        //Store refrences for componenets
        enemybody = gameObject.GetComponent<Rigidbody2D>();
    }

    public override void DetermineMove()
    {
        if(!isTurning && ((Target.position - transform.position).sqrMagnitude <= turnTowardsTargetRange * turnTowardsTargetRange))
        {
            isTurning = true;
        }
        if(isTurning && Target != null)
        {
            Vector3 dir = Target.position - transform.position;
            float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotatePoint);
            rotatePoint += turnSpeed;
        }
        if(SpeedReductionSum < MaximumSlow)
        {
            enemybody.MovePosition(enemybody.position + new Vector2(transform.right.x, transform.right.y) * Speed * (1 - SpeedReductionSum) * Time.fixedDeltaTime);
        }
        else
        {
            enemybody.MovePosition(enemybody.position + new Vector2(transform.right.x, transform.right.y) * Speed * (1 - MaximumSlow) * Time.fixedDeltaTime);
        }
    }
}
