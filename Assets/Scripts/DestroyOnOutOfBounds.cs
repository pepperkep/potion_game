using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnOutOfBounds : MonoBehaviour
{
    public float checkTime;
    public Rect destroyBoundary;
    public string poolReturnName;
    private IEnumerator destroyRoutine;

    // Start is called before the first frame update
    void OnEnable()
    {
        destroyRoutine = ReturnOutOfBounds();
        StartCoroutine(destroyRoutine);
    }

    private IEnumerator ReturnOutOfBounds()
    {
        while(true)
        {
            yield return new WaitForSeconds(checkTime);
            if(!destroyBoundary.Contains(transform.position))
            {
                ObjectPool.Instance.AddToPool(poolReturnName, this.gameObject);
            }
        }
    }
}
