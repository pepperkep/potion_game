using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{

    public bool shaken = false;
    public int shakeThreshold = 200;
    public float maxShakeTime = 2f;
    public int shakeAngle = 30;
    public Color changeColor;
    [SerializeField] private Explosion explosionPrefab;
    private DragDrop dragComponent;
    private int shakeAmount = 0;
    private Vector3 lastPosition;
    private Vector3 lastDisplacment;
    private SpriteRenderer displaySprite;
    private float shakeTime = 0f;
    private bool isShaking = false;
    

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        lastDisplacment = Vector3.zero;
        displaySprite = gameObject.GetComponent<SpriteRenderer>();
        dragComponent = gameObject.GetComponent<DragDrop>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = transform.position - lastPosition;
        if(Vector3.Angle(displacement, lastDisplacment) > shakeAngle)
        {
            shakeAmount++;
            isShaking = true;
        }
        else
        {
            if(shakeTime > maxShakeTime)
            {
                shakeAmount = 0;
                isShaking = false;
                shakeTime = 0;
            }
        }
        lastPosition = transform.position;
        lastDisplacment = displacement;
        if(shakeAmount > shakeThreshold)
        {
            shaken = true;
            displaySprite.color = changeColor;
        }
        if(isShaking)
        {
            shakeTime += Time.deltaTime;
        }
        if(shaken && !dragComponent.GetIsDragging())
        {
            Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
