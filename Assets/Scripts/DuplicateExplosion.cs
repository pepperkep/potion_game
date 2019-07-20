using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateExplosion : MonoBehaviour
{

    [SerializeField] private float timeToDuplicate = 3f;
    [SerializeField] private float additionalZPosition = 1;
    private IEnumerator duplicateRoutine;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Explosion"))
        {
            Explosion explosionToDuplicate = col.gameObject.GetComponent<Explosion>();
            duplicateRoutine = DuplicateInTime(explosionToDuplicate);
            StartCoroutine(duplicateRoutine);
        }
    }

    private IEnumerator DuplicateInTime(Explosion toDuplicate)
    {
        yield return new WaitForSeconds(timeToDuplicate);
        ObjectPool.Instance.SpawnObject(toDuplicate.ExplosionPoolName, new Vector3(transform.position.x, transform.position.y, transform.position.z - additionalZPosition), Quaternion.identity);
    }
}
