using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int startHealth = 20;

    public int StartHealth{
        get => startHealth;
        set => startHealth = value;
    }

    private float currentHealth = 0;
    
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
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Explosion")
        {
            currentHealth -= col.GetComponent<Explosion>().Damage;
        }

    }
}
