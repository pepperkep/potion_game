using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct PotionTypeGenerator
    {
        public string PotionPoolName;
        public float Probability;
    }

    [Tooltip("Time between potion generation attempts. One area must be clear of draggable objects.")]
    [SerializeField] private float timeToGenerate;
    [Tooltip("Number of possible potion spawn points. Higher potion counts decreases spawn area size.")]
    [SerializeField] private int maxPotionCount;
    [Tooltip("List of potions that can be spawned. Potions all have an equal chance of being spanwed.")]
    [SerializeField] private List<PotionTypeGenerator> potionGenerationData;
    private Camera potionCamera;
    private float probabilitySum = 0;


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
    public List<PotionTypeGenerator> PotionGenerationData
    {
        get => potionGenerationData;
        set => potionGenerationData = value;
    }

    private float timeSincePotion = 0f;
    private Vector2 generatorSize;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spriteSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
        generatorSize = spriteSize;
        potionCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        for(int i = 0; i < PotionGenerationData.Count; i++)
        {
            probabilitySum += PotionGenerationData[i].Probability;
        }
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
                CreatePotion(new Vector3(startPlace.x + generatorSize.x/2, startPlace.y + boxAreaHeight/2, -3));

            //Reset timer
            timeSincePotion = 0f;
        }
    }

    public int ChooseRandomPotion()
    {
        float randomNumber = Random.value * probabilitySum;
        for(int i = 0; i < PotionGenerationData.Count; i++)
        {
            if(randomNumber < PotionGenerationData[i].Probability)
            {
                return i;
            }
            else
            {
                randomNumber -= PotionGenerationData[i].Probability;
            }
        }
        return PotionGenerationData.Count - 1;
    }

    //Chose potion randomly from prefab list at potion position
    public void CreatePotion(Vector3 potionPosition)
    {
        string nextPotionPool = PotionGenerationData[ChooseRandomPotion()].PotionPoolName;
        GameObject nextPotion = ObjectPool.Instance.SpawnObject(nextPotionPool, potionPosition, Quaternion.identity);
        nextPotion.GetComponent<DragDrop>().DragCamera = potionCamera;
    }
}
