using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEffect : TempEffect
{
    private float speed;

    public override void FindTarget()
    {
        if(transform.parent != null)
        {
            targetComponent = transform.parent.GetComponent<EnemyMovement>();
        }
        else
        {
            targetComponent = null;
        }
    }

    public override void ApplyTick()
    {
        if(targetComponent != null)
        {
            ((EnemyMovement)targetComponent).NumberOfStuns++;
        }
    }

    public override void EndEffect()
    {
        if(targetComponent != null)
        {
            ((EnemyMovement)targetComponent).NumberOfStuns--;
        }
    }
}
