    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Tooltip("Time explosion radius is active")]
    [SerializeField] private float duration = 50f;
    [SerializeField] private string explosionPoolName = "";
    [Tooltip("Extra blue for a duplicate explosion")]
    [SerializeField] private float duplicateBlueAmount = 20f;
    private bool duplicate = false;
    private SpriteRenderer renderComponenet;

    public float Duration
    {
        get => duration;
        set => duration = value;
    }
    public string ExplosionPoolName
    {
        get => explosionPoolName;
        set => explosionPoolName = value;
    }
    public bool Duplicate
    {
        get => duplicate;
        set
        {
            duplicate = value;
            if(renderComponenet != null)
            {
                Color tempColor = renderComponenet.color;
                if(duplicate)
                {
                    tempColor.b += duplicateBlueAmount;
                }
                else
                {
                    tempColor.b -= duplicateBlueAmount;
                }
                renderComponenet.color = tempColor;
            }
        }
    }

    //Time explosion has been on screen
    private float timeSpent = 0;

    void Awake()
    {
        renderComponenet = gameObject.GetComponent<SpriteRenderer>();
    }

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
            Duplicate = false;
            ObjectPool.Instance.AddToPool(explosionPoolName, this.gameObject);
        }
    }
}
