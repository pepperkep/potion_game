using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float damage = 5f;
    [SerializeField] private bool damagePlayer;
    [SerializeField] private bool damageEnemies;
    [SerializeField] private bool destroyonDamage = false;
    [SerializeField] private string poolName = null;
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
        get => destroyonDamage;
        set => destroyonDamage = value;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.CompareTag("Target") && DamagePlayer) || (col.tag == "Enemy" && DamageEnemies))
        {
            col.GetComponent<Damageable>().CurrentHealth -= damage;
            if(DestroyOnDamage)
            {
                if(poolName != "")
                {
                    ObjectPool.Instance.AddToPool(poolName, this.gameObject);
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }

    }
}
