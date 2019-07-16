using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maximumSlow = 0.94f;
    private float speedReductionSum = 0;
    private int numberOfStuns = 0;
    
    abstract public Transform Target{get; set;}
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    public int NumberOfStuns
    {
        get => numberOfStuns;
        set => numberOfStuns = value;
    }
    public float SpeedReductionSum
    {
        get => speedReductionSum;
        set => speedReductionSum = value;
    }
    public float MaximumSlow
    {
        get => maximumSlow;
        set => maximumSlow = value;
    }

    void OnEnable()
    {
        NumberOfStuns = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(NumberOfStuns == 0)
        {
            DetermineMove();
        }
    }

    public abstract void DetermineMove();
}
