using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    [SerializeField] private string deathExplosionPool = "";
    [SerializeField] private string returnPoolName = "";

    public void ReleaseExplosion()
    {
        ObjectPool.Instance.SpawnObject(deathExplosionPool, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1), this.transform.rotation);
        ObjectPool.Instance.AddToPool(returnPoolName, this.gameObject);
    }
}
