using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMovement : Movement
{
    [SerializeField] private float turnTowardsTargetRange = 4f;
    
    private bool isTurning = false;
    private Transform target;
    
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

    void OnEnable()
    {
        isTurning = false;
    }

    public override void DetermineMove()
    {
        if(Target != null)
        {
            if(!isTurning && ((Target.position - transform.position).sqrMagnitude <= turnTowardsTargetRange * turnTowardsTargetRange))
            {
                isTurning = true;
                TurnRoutine = TurnTowards(Target.position);
            }
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
