using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int Size; 
    }
    public static ObjectPool instance;
    private void Awake()
    {
        instance = this;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i <pool.Size; i++)
            {
               GameObject obj= Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag,Vector3 position,Quaternion Rotations)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag" + tag + "doesn't exist.");
            return null;
        }
        GameObject ObjectToSpawn=poolDictionary[tag].Dequeue();
        ObjectToSpawn.SetActive(true);
        ObjectToSpawn.transform.position = position;
        ObjectToSpawn.transform.rotation = Rotations;
        poolDictionary[tag].Enqueue(ObjectToSpawn);
        return ObjectToSpawn;
    }
}
