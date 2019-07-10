using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionGenerator : MonoBehaviour
{

    [Tooltip("Time between potion generation attempts. One area must be clear of draggable objects.")]
    [SerializeField] private float timeToGenerate;
    [Tooltip("Number of possible potion spawn points. Higher potion counts decreases spawn area size.")]
    [SerializeField] private int maxPotionCount;
    [Tooltip("List of potions that can be spawned. Potions all have an equal chance of being spanwed.")]
    [SerializeField] private List<GameObject> potionPrefabs;
    private Camera potionCamera;


    //Properties corresponding to serialized fields
    public float TimeToGenerate
    {
        get  => timeToGenerate;
        set => timeToGenerate = value;
    }
    public int MaxPotionCount
    {
        get => maxPotionCount;
        set => maxPotionCount = value;
    }
    public List<GameObject> PotionPrefabs
    {
        get => potionPrefabs;
        set => potionPrefabs = value;
    }

    private float timeSincePotion = 0f;
    private Vector2 generatorSize;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spriteSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
        generatorSize = spriteSize;
        potionCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSincePotion += Time.deltaTime;
        if(timeSincePotion > TimeToGenerate)
        {
            //loop initializtion variables for overlap area calls
            float boxAreaHeight = generatorSize.y / MaxPotionCount;
            Vector2 startPlace = new Vector2(transform.position.x - generatorSize.x/2, transform.position.y - generatorSize.y/2);
            Vector2 endPlace = new Vector2(transform.position.x + generatorSize.x/2, startPlace.y + boxAreaHeight);
            float endHeight = transform.position.y + generatorSize.y /2;
            
            //Find a potion spawn area with no draggable objects
            while(Physics2D.OverlapArea(startPlace, endPlace, LayerMask.GetMask("DragDrop")) != null && startPlace.y < endHeight)
            {
                startPlace.y += boxAreaHeight;
                endPlace.y += boxAreaHeight;
            }

            //If a spawn location is found spawn a potion
            if(startPlace.y < endHeight)
                CreatePotion(new Vector3(startPlace.x + generatorSize.x/2, startPlace.y + boxAreaHeight/2, -2));

            //Reset timer
            timeSincePotion = 0f;
        }
    }

    //Chose potion randomly from prefab list at potion position
    public void CreatePotion(Vector3 potionPosition)
    {
        GameObject nextPotion = PotionPrefabs[Random.Range(0, PotionPrefabs.Count)];
        nextPotion = Instantiate(nextPotion, potionPosition, Quaternion.identity);
        nextPotion.GetComponent<DragDrop>().DragCamera = potionCamera;
    }
}
