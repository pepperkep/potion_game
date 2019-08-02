using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnDisable : MonoBehaviour
{
    [SerializeField] private string instantiateObjectPool = "";
    
    void OnDisable()
    {
        ObjectPool.Instance.SpawnObject(instantiateObjectPool, transform.position, Quaternion.identity);
    }
}
