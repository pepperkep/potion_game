using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion_Generator : MonoBehaviour
{

    [SerializeField] private float timeToGenerate;
    [SerializeField] private int maxPotionCount;
    [SerializeField] private List<GameObject> potionPrefabs;

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
        generatorSize = spriteSize;//new Vector2(spriteSize.x * transform.localScale.x, spriteSize.y * transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        timeSincePotion += Time.deltaTime;
        if(timeSincePotion > TimeToGenerate)
        {
            float boxAreaHeight = generatorSize.y / MaxPotionCount;
            Vector2 startPlace = new Vector2(transform.position.x - generatorSize.x/2, transform.position.y - generatorSize.y/2);
            Vector2 endPlace = new Vector2(transform.position.x + generatorSize.x/2, startPlace.y + boxAreaHeight);
            float endHeight = transform.position.y + generatorSize.y /2;
            while(Physics2D.OverlapArea(startPlace, endPlace, LayerMask.GetMask("DragDrop")) != null && startPlace.y < endHeight)
            {
                startPlace.y += boxAreaHeight;
                endPlace.y += boxAreaHeight;
            }
            if(startPlace.y < endHeight)
                CreatePotion(new Vector3(startPlace.x + generatorSize.x/2, startPlace.y + boxAreaHeight/2, -1));
            timeSincePotion = 0f;
        }
    }

    public void CreatePotion(Vector3 potionPosition)
    {
        GameObject nextPotion = PotionPrefabs[Random.Range(0, PotionPrefabs.Count)];
        nextPotion = Instantiate(nextPotion, potionPosition, Quaternion.identity);
        nextPotion.gameObject.GetComponent<DragDrop>().DragCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
}
