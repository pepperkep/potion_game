using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{


    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }

    public Dictionary<string, Queue<GameObject>> poolsDictionary;
    public List<Pool> pools;

    public static ObjectPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        poolsDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, this.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolsDictionary.Add(pool.name, objectPool);
        }
        
    }

    public GameObject SpawnObject(string name, Vector3 position, Quaternion rotation)
    {
        if(!poolsDictionary.ContainsKey(name))
        {
            Debug.LogWarning("Pool name " + name + " does not exit");
            return null;
        }

        GameObject spawnObject;
        if(poolsDictionary[name].Count > 0)
        {
            spawnObject = poolsDictionary[name].Dequeue();
        }
        else
        {
            GameObject prefabToSpawn = null;
            foreach(Pool pool in pools)
            {
                if(pool.name == name)
                    prefabToSpawn = pool.prefab;
            }
            spawnObject = Instantiate(prefabToSpawn);
        }

        spawnObject.SetActive(true);
        spawnObject.transform.position = position;
        spawnObject.transform.rotation = rotation;
        spawnObject.transform.parent = null;

        return spawnObject;
    }

    public void AddToPool(string name, GameObject objectToAdd)
    {
        poolsDictionary[name].Enqueue(objectToAdd);
        objectToAdd.SetActive(false);
        objectToAdd.transform.parent = this.transform;
    }

}
