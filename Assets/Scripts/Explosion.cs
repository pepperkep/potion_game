using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Tooltip("Time explosion radius is active")]
    [SerializeField] private float duration = 50f;
    [SerializeField] private string explosionPoolName = "";
    [SerializeField] private bool stuns = false;

    public float Duration
    {
        get => duration;
        set => duration = value;
    }
    public bool Stuns
    {
        get => stuns;
        set => stuns = value;
    }

    //Time explosion has been on screen
    private float timeSpent = 0;

    void OnEnable()
    {
        timeSpent = 0;
    }

    // Destroy potion when tinme exceeds duration
    void Update()
    {
        timeSpent += Time.deltaTime;
        if(timeSpent > Duration)
        {
            ObjectPool.Instance.AddToPool(explosionPoolName, this.gameObject);
        }
    }
}
