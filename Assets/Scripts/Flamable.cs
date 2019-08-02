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
            Explosion flameStart = col.gameObject.GetComponent<Explosion>();
            if(flameStart != null && flameStart.StartsFires)
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
