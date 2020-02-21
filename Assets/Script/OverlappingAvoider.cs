using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlappingAvoider : MonoBehaviour
{
    ObjectPool Pool;
    // Start is called before the first frame update
    private void Start()
    {
        Pool = ObjectPool.instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Health" || other.tag == "Point" || other.tag == "Fire" || other.tag == "Spike")
        {
            other.gameObject.SetActive(false);
            Pool.poolDictionary[other.tag].Enqueue(gameObject);
        }
    }
}
