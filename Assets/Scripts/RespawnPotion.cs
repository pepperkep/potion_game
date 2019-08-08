using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPotion : MonoBehaviour
{
    [SerializeField] private string potionPoolName;
    [SerializeField] private float respawnableTime = 5.0f;
    private IEnumerator destroyRoutine = null;

    public string PotionPoolName
    {
        get => potionPoolName;
        set => potionPoolName = value;
    }

    void Start()
    {
        destroyRoutine = DestroyRespawn();
        StartCoroutine(destroyRoutine);
    }

    public void RespawnPreviousPotion()
    {
        ObjectPool.Instance.SpawnObject(PotionPoolName, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    IEnumerator DestroyRespawn()
    {
        yield return new WaitForSeconds(respawnableTime);
        Destroy(this.gameObject);
    }
}
