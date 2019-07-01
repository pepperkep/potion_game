using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int startHealth = 20;
    [SerializeField] private float currentHealth = 0;
    [SerializeField] private string poolName = null;

    public int StartHealth
    {
        get => startHealth;
        set => startHealth = value;
    }
    public float CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = StartHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
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
