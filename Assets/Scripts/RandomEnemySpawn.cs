using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;

    void Start()
    {
        for (int i = 0; i < 3; i++) {
            Vector3 spawnPos = new Vector3(Random.Range(5, 15), Random.Range(-1, 5), 0);
            Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
        }
    }
}
