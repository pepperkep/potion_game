using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int startHealth = 20;
    [SerializeField] private int currencyOnDeath = 0;
    [SerializeField] private float damageMultiplier = 1f;
    private float currentHealth = 0;
    [Tooltip("Pool object will return to. Object will be destroyed if pool name is an empty string.")]
    [SerializeField] private string returnPoolName = null;
    [Tooltip("Percentage size when hit by size explosion")]
    [SerializeField] private float smallPercentage = 0.2f;
    private bool deathOnNextHit = false;

    public int StartHealth
    {
        get => startHealth;
        set => startHealth = value;
    }
    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            float damageDealt = currentHealth - value;
            currentHealth -= damageDealt * DamageMultiplier;
            if(DeathOnNextHit)
            {
                transform.localScale /= smallPercentage;
                Kill();
            }
        }
    }
    public int CurrencyOnDeath
    {
        get => currencyOnDeath;
        set => currencyOnDeath = value;
    }
    public float DamageMultiplier
    {
        get => damageMultiplier;
        set => damageMultiplier = value;
    }
    public bool DeathOnNextHit
    {
        get => deathOnNextHit;
        set
        {
            deathOnNextHit = value;
            transform.localScale *= smallPercentage;
        }
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
        int numberOfChildren = transform.childCount;
        int childNumber = 0;
        for(int i = 0; i < numberOfChildren; i++, childNumber++)
        {
            TempEffect childEffect = transform.GetChild(childNumber).GetComponent<TempEffect>();
            if(childEffect != null)
            {
                childEffect.KillEffect();
                childNumber--;
            }
            else
            {
                Worm wormOnEnemy = transform.GetChild(childNumber).GetComponent<Worm>();
                if(wormOnEnemy != null)
                {
                    wormOnEnemy.ReleaseExplosion();
                    childNumber--;
                }
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
