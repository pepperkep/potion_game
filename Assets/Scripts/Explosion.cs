using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float duration = 50f;
    private float timeSpent = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime;
        if(timeSpent > duration)
        {
            Destroy(this.gameObject);
        }
    }
}
