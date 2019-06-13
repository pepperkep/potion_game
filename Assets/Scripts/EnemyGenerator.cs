﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private float baseSpawnRate;
    [SerializeField] private float spawnRateMultiplier;
    [SerializeField] private float spawnRateMultipyTime;
    [SerializeField] private float minEnemyHeight;
    [SerializeField] private float maxEnemyHeight;

    public List<GameObject> EnemyPrefabs
    {
        get => enemyPrefabs;
        set => enemyPrefabs = value;
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
    
    private float currentSpawnRate;
    private float timeSinceSpawn = 0;
    private float timeSinceRateChange = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnRate = BaseSpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        timeSinceRateChange += Time.deltaTime;
        if(timeSinceSpawn > currentSpawnRate)
        {
            Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)], new Vector3(transform.position.x, Random.Range(MinEnemyHeight, MaxEnemyHeight), transform.position.z), Quaternion.identity);
            timeSinceSpawn = 0;
        }
        if(timeSinceRateChange > SpawnRateMultipyTime)
        {
            currentSpawnRate *= SpawnRateMultiplier;
            timeSinceRateChange = 0;
        }
    }
}
