using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkEffect : TempEffect
{
    [SerializeField] private float speedAmount = 1.2f;
    private Movement movementComponenet = null;

    public override void FindTarget()
    {
        if(transform.parent != null)
        {
            targetComponent = transform.parent.GetComponent<Damageable>();
            movementComponenet = transform.parent.GetComponent<Movement>();
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
            ((Damageable)targetComponent).DeathOnNextHit = true;
            movementComponenet.SpeedReductionSum -= speedAmount;
        }
    }

    public override void EndEffect()
    {
        if(targetComponent != null)
        {
            ((Damageable)targetComponent).DeathOnNextHit = false;
            movementComponenet.SpeedReductionSum += speedAmount;
        }
    }
}
