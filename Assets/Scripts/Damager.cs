using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float damage = 5f;
    [SerializeField] private bool damagePlayer;
    [SerializeField] private bool damageEnemies;
    [SerializeField] private bool destroyOnDamage = false;
    [Tooltip("Pool object will return to. Object will be destroyed if pool name is an empty string.")]
    [SerializeField] private string returnPoolName = null;
    [SerializeField] private string debuffPoolName = null;
    public float Damage
    {
        get => damage;
        set => damage = value;
    }
    public bool DamagePlayer
    {
        get => damagePlayer;
        set => damagePlayer = value;
    }
    public bool DamageEnemies
    {
        get => damageEnemies;
        set => damageEnemies = value;
    }
    public bool DestroyOnDamage
    {
        get => destroyOnDamage;
        set => destroyOnDamage = value;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if((col.gameObject.CompareTag("Target") && DamagePlayer) || (col.gameObject.CompareTag("Enemy") && DamageEnemies))
        {
            col.gameObject.GetComponent<Damageable>().CurrentHealth -= damage;
            if(col.gameObject.CompareTag("Enemy") && DamageEnemies && debuffPoolName != "")
            {
                ObjectPool.Instance.SpawnObject(debuffPoolName, transform.position, transform.rotation, col.transform).GetComponent<TempEffect>();
            }
            if(DestroyOnDamage)
            {
                if(returnPoolName != "")
                {
                    ObjectPool.Instance.AddToPool(returnPoolName, this.gameObject);
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }

    }
}
