using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargeEffect : TempEffect
{
    [SerializeField] private float scaleIncrease = 1.1f;
    [SerializeField] private float damageDebuff = 1.1f;
    public override void FindTarget()
    {
        if(transform.parent != null)
        {
            targetComponent = transform.parent.GetComponent<Damageable>();
            if(targetComponent != null)
            {
                transform.parent.localScale *= scaleIncrease;
            }
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
            ((Damageable)targetComponent).DamageMultiplier += damageDebuff;
        }
    }

    public override void EndEffect()
    {
        if(targetComponent != null)
        {
            transform.parent.localScale /= scaleIncrease;
            ((Damageable)targetComponent).DamageMultiplier -= damageDebuff;
        }
    }
}
