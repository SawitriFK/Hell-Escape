using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRndom : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints = null;
    [SerializeField] private GameObject[] enemyPrefabs = null;
    [SerializeField] private int enemy = 0;


    void Start()
    { 
        int i = 0;
        int temp = -1;

        while (i < enemy)
        {
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            if(randSpawnPoint != temp)
            {
                Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
                temp= randSpawnPoint;
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
