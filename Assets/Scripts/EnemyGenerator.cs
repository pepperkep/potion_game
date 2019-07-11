using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    [SerializeField] private List<string> enemyPoolNames;
    [SerializeField] private float baseSpawnRate;
    [SerializeField] private float spawnRateMultiplier;
    [SerializeField] private float spawnRateMultipyTime;
    [SerializeField] private float minEnemyHeight;
    [SerializeField] private float maxEnemyHeight;
    [SerializeField] private int burstNumber;
    [SerializeField] private float burstMultiplier;
    [SerializeField] private float burstTime;
    [SerializeField] private float burstInterval;
    [SerializeField] private bool continueSpawning = true;
    [SerializeField] private GameObject enemyTarget;

    public List<string> EnemyPoolNames
    {
        get => enemyPoolNames;
        set => enemyPoolNames = value;
    }
    public float BaseSpawnRate
    {
        get => baseSpawnRate;
        set => baseSpawnRate = value;
    }
    public float SpawnRateMultiplier
    {
        get => spawnRateMultiplier;
        set => spawnRateMultiplier = value;
    }
    public float SpawnRateMultipyTime
    {
        get => spawnRateMultipyTime;
        set => spawnRateMultipyTime = value;
    }
    public float MinEnemyHeight
    {
        get => minEnemyHeight;
        set => minEnemyHeight = value;
    }
    public float MaxEnemyHeight
    {
        get => maxEnemyHeight;
        set => maxEnemyHeight = value;
    }
    public int BurstNumber
    {
        get => burstNumber;
        set => burstNumber = value;
    }
    public float BurstMultiplier
    {
        get => burstMultiplier;
        set => burstMultiplier = value;
    }
    public float BurstTime
    {
        get => burstTime;
        set => burstTime = value;
    }
    public float BurstInterval
    {
        get => burstInterval;
        set => burstInterval = value;
    }
    public bool ContinueSpawning
    {
        get => continueSpawning;
        set => continueSpawning = value;
    }
    public GameObject EnemyTarget
    {
        get => enemyTarget;
        set => enemyTarget = value;
    }
    
    private float currentSpawnRate;
    private float timeSinceSpawn = 0;
    private float timeSinceRateChange = 0;
    private float currentBurstAmount;
    private float timeSinceBurst = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnRate = BaseSpawnRate;
        currentBurstAmount = BurstNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if(ContinueSpawning){
            timeSinceSpawn += Time.deltaTime;
            timeSinceRateChange += Time.deltaTime;
            timeSinceBurst += Time.deltaTime;
            if(timeSinceSpawn > currentSpawnRate)
            {
                SpawnEnemy(EnemyPoolNames[Random.Range(0, EnemyPoolNames.Count)], Random.Range(MinEnemyHeight, MaxEnemyHeight));
                timeSinceSpawn = 0;
            }
            if(timeSinceRateChange > SpawnRateMultipyTime)
            {
                currentSpawnRate *= SpawnRateMultiplier;
                timeSinceRateChange = 0;
            }
            if(timeSinceBurst > BurstTime)
            {
                timeSinceBurst = 0;
                StartCoroutine("EnemyBurst", currentBurstAmount);
                currentBurstAmount *= BurstMultiplier;
            }
        }
    }

    public void SpawnEnemy(string typeName, float height)
    {
        GameObject newEnemy = ObjectPool.Instance.SpawnObject(typeName, new Vector3(transform.position.x, height, transform.position.z), Quaternion.identity);
        newEnemy.GetComponent<EnemyMovement>().Target = EnemyTarget.transform;
    }

    public IEnumerator EnemyBurst(int enemyNumber)
    {
        for(int i = 0; i < enemyNumber; i++)
        {
            SpawnEnemy(EnemyPoolNames[Random.Range(0, EnemyPoolNames.Count)], Random.Range(MinEnemyHeight, MaxEnemyHeight));
            yield return new WaitForSeconds(BurstInterval);
        }
    }
}
