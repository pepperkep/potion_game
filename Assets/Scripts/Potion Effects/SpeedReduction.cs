using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedReduction : MonoBehaviour
{
    [SerializeField] private float slowAmount;
    public float SlowAmount
    {
        get => slowAmount;
        set => slowAmount = value;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            Movement enemyMovement = col.gameObject.GetComponent<Movement>();
            enemyMovement.SpeedReductionSum += SlowAmount; 
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            Movement enemyMovement = col.gameObject.GetComponent<Movement>();
            enemyMovement.SpeedReductionSum -= SlowAmount; 
        }
    }
}
