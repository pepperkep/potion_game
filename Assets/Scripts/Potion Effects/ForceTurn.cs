using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceTurn : MonoBehaviour
{

    [SerializeField] private List<Transform> turnTowardsPoints = new List<Transform>();
    [SerializeField] private bool stopAfterReachingTransform = true;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            Movement enemyMovement = col.gameObject.GetComponent<Movement>();
            enemyMovement.TurnRoutine = enemyMovement.TurnTowards(FindClosestTurnPoint(col.transform.position), stopAfterReachingTransform);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            Movement enemyMovement = col.gameObject.GetComponent<Movement>();
            if(enemyMovement.Target != null)
            {
                enemyMovement.TurnRoutine = enemyMovement.TurnTowards(enemyMovement.Target);
            }
        }
    }

    public Transform FindClosestTurnPoint(Vector3 enemyPosition)
    {
        float lowestDistanceSquared = float.PositiveInfinity;
        Transform closestTransform = null;
        for(int i = 0; i < turnTowardsPoints.Count; i++)
        {
            if((turnTowardsPoints[i].position - enemyPosition).sqrMagnitude < lowestDistanceSquared)
            {
                lowestDistanceSquared = (turnTowardsPoints[i].position - enemyPosition).sqrMagnitude;
                closestTransform = turnTowardsPoints[i];
            }
        }
        return closestTransform;
    }
}
