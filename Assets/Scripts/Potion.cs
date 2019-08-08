using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{

    [Tooltip("Potion will explode when released if shaken")]
    [SerializeField] private bool shaken = false;
    [Tooltip("Color of shaken explosion")]
    [SerializeField] private Color changeColor;
    [Tooltip("Explosion instantiated when shaken potion is released")]
    [SerializeField] private string explosionPoolName = "";
    [SerializeField] private GameObject respawnMarker;
    [SerializeField] private string returnPoolName = "";
    [Header("Potion Shake Requirements")]
    [SerializeField] private bool directInputToShake = true;
    [Tooltip("Times direction must be changed to shake potion")]
    [SerializeField] private float shakeThreshold = 200;
    [Tooltip("Time all direction changes must happen to be shaken. Times shaken resets after this amount of time.")]
    [SerializeField] private float maxShakeTime = 2f;
    [Tooltip("Minimum angle of direction change for it to count towards the shake threshold.")]
    [SerializeField] private int shakeAngle = 30;
    [Tooltip("Time for potion to explode after being triggered by another explosion")]
    [SerializeField] private float necessarySpeed = 3f;
    [SerializeField] private int speedThreshold = 20;
    [SerializeField] private float fuseTime = 2.0f;
    [SerializeField] private int explosionColorChanges = 10;

    public bool Shaken{
        get => shaken;
        set => shaken = value;
    }
    public Color ChangeColor{
        get => changeColor;
        set => changeColor = value;
    }
    public float ShakeThreshold
    {
        get => shakeThreshold;
        set => shakeThreshold = value;
    } 
    public float MaxShakeTime
    {
        get => maxShakeTime;
        set => maxShakeTime = value;
    }
    public int ShakeAngle
    {
        get => shakeAngle;
        set => shakeAngle = value;
    }
    public float FuseTime{
        get => fuseTime;
        set => fuseTime = value;
    }
    public float NecessarySpeed
    {
        get => necessarySpeed;
        set => necessarySpeed = value;
    }
    public int SpeedThreshold
    {
        get => speedThreshold;
        set => speedThreshold = value;
    }

    //Drag drop component to get if potion is being dragged
    private DragDrop dragComponent;
    //current shaken amount
    private float shakeAmount = 0;
    private int speedAmount = 0;
    //Last position to determine displacement next update
    private Vector3 lastPosition;
    //Last displament to determine if direction has changed
    private Vector3 lastDisplacment;
    //Sprite rendering component to effect sprite on shaken
    private SpriteRenderer displaySprite;
    //Amount of time since shaking started
    private float shakeTime = 0f;
    //Whether currently in the state of shaking
    private bool isShaking = false;

    // Start is called before the first frame update
    void Start()
    {
        displaySprite = gameObject.GetComponent<SpriteRenderer>();
        dragComponent = gameObject.GetComponent<DragDrop>();
    }

    void OnEnable()
    {
        lastPosition = transform.position;
        lastDisplacment = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(!directInputToShake)
        {
            //Determine current displacement
            Vector3 displacement = transform.position - lastPosition;
            float speed = displacement.magnitude / Time.deltaTime;

            //Increase shake amount if angle is large enough
            if(Vector3.Angle(displacement, lastDisplacment) > ShakeAngle)
            {
                shakeAmount++;
                isShaking = true;
            }
            if(speed > NecessarySpeed)
            {
                speedAmount++;
            }

            //Store information needed for next update
            lastPosition = transform.position;
            lastDisplacment = displacement;
        }
        else
        {
            if(dragComponent.GetIsDragging() && Input.GetKey("mouse 1"))
            {
                shakeAmount += Time.deltaTime;
                isShaking = true;
            }
        }
        //Reset shake amount if too much time has passed
        if(shakeTime > MaxShakeTime)
        {
            shakeAmount = 0;
            isShaking = false;
            shakeTime = 0;
            speedAmount = 0;
        }

        //Change state to shaken if shake threshold has been reached
        if(shakeAmount > ShakeThreshold && (directInputToShake || speedAmount > SpeedThreshold))
        {
            Shaken = true;
            displaySprite.color = ChangeColor;
        }

        //Update time shince shaking startyed
        if(isShaking)
        {
            shakeTime += Time.deltaTime;
        }

        //Explode if shaken and no longer being dragged
        if(Shaken && !dragComponent.GetIsDragging())
        {
            Explode();
        }
    }


    //Explode on contact with another expolsion
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Explosion"))
        {
            StartCoroutine("ExplodeAfterTime", FuseTime);
        }
    }

    public IEnumerator ExplodeAfterTime(float timeToExplode)
    {
        Color startColor = displaySprite.color;
        float waitTime = timeToExplode / explosionColorChanges;
        for(int i = 0; i < explosionColorChanges; i++)
        {
            if(i % 2 == 0)
            {
                displaySprite.color = changeColor;
            }
            else
            {
                displaySprite.color = startColor;
            }
            yield return new WaitForSeconds(waitTime);
        }
        Explode();
    }

    //Instantiate explosion
    public void Explode()
    {
        ObjectPool.Instance.SpawnObject(explosionPoolName, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1), this.transform.rotation);
        GameObject marker = Instantiate(respawnMarker, transform.position, Quaternion.identity);
        RespawnPotion respawnComponent = marker.GetComponent<RespawnPotion>();
        if(respawnComponent != null)
        {
            respawnComponent.PotionPoolName = returnPoolName;
        }
        ObjectPool.Instance.AddToPool(returnPoolName, this.gameObject);
    }
}
