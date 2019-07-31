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
    [SerializeField] private float rotatePerAlchol = 10f;
    [SerializeField] private float drunkMulitplier = 6f;
    private float alcoholContent = 0;
    private float rotatePoint = 0;
    private float speedReductionSum = 0;
    private int numberOfStuns = 0;
    private IEnumerator drunkRoutine;
    
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
    public float AlcoholContent
    {
        get => alcoholContent;
        set => alcoholContent = value;
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
        if(drunkRoutine == null && AlcoholContent > 0)
        {
            drunkRoutine = DrunkEnemy();
            StartCoroutine(drunkRoutine);
        }
        else
        {
            if(AlcoholContent < 0)
            {
                StopCoroutine(drunkRoutine);
                drunkRoutine = null;
            }
        }
    }

    public IEnumerator TurnTowards(Vector3 turnTarget)
    {
        while(rotatePoint < 1 && turnTarget != null)
        {
            Vector3 dir = turnTarget - transform.position;
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

    public IEnumerator DrunkEnemy()
    {
        float currentTime = 0;
        Quaternion leftRotate;
        Quaternion rightRotate;
        while(AlcoholContent > 0)
        {
            rightRotate = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + AlcoholContent * rotatePerAlchol);
            leftRotate = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - AlcoholContent * rotatePerAlchol);
            // 30 degrees added to start at the middle rotation and the addition of one and divide by two to change range from -1 to 1 over to 0 to 1
            transform.rotation = Quaternion.Slerp(leftRotate, rightRotate, (Mathf.Sin(currentTime * drunkMulitplier + 30 * Mathf.Deg2Rad) + 1) / 2);
            yield return new WaitForSeconds(turnInterval);
            currentTime += turnInterval;
        }
        TurnTowards(Target.position);
    }

    public abstract void DetermineMove();
}
