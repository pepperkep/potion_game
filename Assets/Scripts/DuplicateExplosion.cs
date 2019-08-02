using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateExplosion : MonoBehaviour
{

    [SerializeField] private float timeToDuplicate = 3f;
    [SerializeField] private float additionalZPosition = 1;
    [Tooltip("Time after duplicated explosion where the duplicator cannot take additional explosion input")]
    private IEnumerator duplicateRoutine;
    private bool isDuplicating = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Explosion"))
        {
            if(!isDuplicating){
                Explosion explosionToDuplicate = col.gameObject.GetComponent<Explosion>();
                if(explosionToDuplicate != null && !explosionToDuplicate.Duplicate)
                {
                    duplicateRoutine = DuplicateInTime(explosionToDuplicate);
                    StartCoroutine(duplicateRoutine);
                    isDuplicating = true;
                }
            }
        }
    }

    private IEnumerator DuplicateInTime(Explosion toDuplicate)
    {
        yield return new WaitForSeconds(timeToDuplicate);
        GameObject spawnedObject = ObjectPool.Instance.SpawnObject(toDuplicate.ExplosionPoolName, new Vector3(transform.position.x, transform.position.y, transform.position.z - additionalZPosition), Quaternion.identity);
        spawnedObject.GetComponent<Explosion>().Duplicate = true;
        isDuplicating = false;
    }
}
