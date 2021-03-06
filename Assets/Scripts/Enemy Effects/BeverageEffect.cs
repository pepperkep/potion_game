﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeverageEffect : TempEffect
{
    [SerializeField] private float slowAmount = 0.1f;

    public override void FindTarget()
    {
        if(transform.parent != null)
        {
            targetComponent = transform.parent.GetComponent<Movement>();
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
            ((Movement)targetComponent).AlcoholContent++;
            ((Movement)targetComponent).SpeedReductionSum += slowAmount;
        }
    }

    public override void EndEffect()
    {
        if(targetComponent != null)
        {
            ((Movement)targetComponent).AlcoholContent--;
            ((Movement)targetComponent).SpeedReductionSum -= slowAmount;
        }
    }
}
