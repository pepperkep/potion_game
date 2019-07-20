using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int startHealth = 20;
    [SerializeField] private int currencyOnDeath = 0;
    private float currentHealth = 0;
    [Tooltip("Pool object will return to. Object will be destroyed if pool name is an empty string.")]
    [SerializeField] private string returnPoolName = null;

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
    public int CurrencyOnDeath
    {
        get => currencyOnDeath;
        set => currencyOnDeath = value;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHealth = StartHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Kill();
            GameStatusManager.Instance.Parts += CurrencyOnDeath;
        }
    }

    public void Kill()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            TempEffect childEffect;
            if((childEffect = transform.GetChild(i).GetComponent<TempEffect>()) != null)
            {
                childEffect.KillEffect();
            }
        }
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
