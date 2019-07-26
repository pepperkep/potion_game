using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotEffect : TempEffect
{

    [SerializeField] private float damagePerTick = 0.05f;
    public override void FindTarget()
    {
        if(transform.parent != null)
        {
            targetComponent = transform.parent.GetComponent<Damageable>();
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
            ((Damageable)targetComponent).CurrentHealth -= damagePerTick;
        }
    }

    public override void EndEffect()
    {

    }
}
