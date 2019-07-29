using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maximumSlow = 0.94f;
    [SerializeField] private float turnSpeed = 0.1f;
    [SerializeField] private float turnInterval = 0.01f;
    [SerializeField] private IEnumerator turnRoutine;
    [SerializeField] private float minStopTurn = 0.001f;
    private float rotatePoint = 0;
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
    public float TurnSpeed
    {
        get => turnSpeed;
        set => turnSpeed = value;
    }
    public float TurnInterval
    {
        get => turnInterval;
        set => turnInterval = value;
    }
    public IEnumerator TurnRoutine
    {
        get => turnRoutine;
        set
        {
            if(turnRoutine != null)
            {
                StopCoroutine(turnRoutine);
                rotatePoint = 0;
            }
            if(gameObject.activeInHierarchy)
            {
                turnRoutine = value;
                StartCoroutine(turnRoutine);
            }
        }
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

    public IEnumerator TurnTowards(Transform turnTarget)
    {
        while(rotatePoint < 1 && turnTarget != null)
        {
            Vector3 dir = turnTarget.position - transform.position;
            if(dir.sqrMagnitude > minStopTurn)
            {
                float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotatePoint);
                rotatePoint += turnSpeed;
                yield return new WaitForSeconds(turnInterval);
            }
            else
            {
                rotatePoint = 1;
            }
        }
        rotatePoint = 0;
    }

    public abstract void DetermineMove();
}
