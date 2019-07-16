using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    [SerializeField] private Transform target;
    
    public override Transform Target
    {
        get => target;
        set
        {
            target = value;
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private Rigidbody2D enemybody;

    void Start()
    {
        //Store refrences for componenets
        enemybody = gameObject.GetComponent<Rigidbody2D>();
    }

    public override void DetermineMove()
    {
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
