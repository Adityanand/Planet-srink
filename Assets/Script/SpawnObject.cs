using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    GameObject Player;
    ObjectPool Pool;
    Vector3 offset;
    public string[] tags;
    int Index;
    void Start()
    {
        Pool = ObjectPool.instance;
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnObj());
        offset = new Vector3(0, -.5f, 0);
    }
    private void Update()
    {
        if (Player == null)
            Debug.LogWarning("Player Died");
    }
    IEnumerator SpawnObj()
    {
        yield return new WaitForSeconds(1);
        Index = Random.Range(0, tags.Length);
        Debug.Log(Index);
        Pool.SpawnFromPool(tags[Index],- Player.transform.position+offset, Quaternion.Euler(90,0,0));
        yield return new WaitForSeconds(4);
        StartCoroutine(SpawnObj());
    }
}

