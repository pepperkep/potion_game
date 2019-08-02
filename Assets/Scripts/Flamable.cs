using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamable : MonoBehaviour
{
    [SerializeField] private GameObject flameObject;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Explosion"))
        {
            if(col.gameObject.GetComponent<Explosion>().StartsFires)
            {
                flameObject.SetActive(true);
            }
        }
    }

    void OnEnable()
    {
        flameObject.SetActive(false);
    }
}
