using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints = null;
    [SerializeField] private GameObject[] enemyPrefabs = null;
    [SerializeField] private int enemy;
    [SerializeField] private bool toSpwan = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int randEnemy = Random.Range(0, enemyPrefabs.Length);
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);

        if(toSpwan == true)
        {
            Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
            toSpwan = false;
            StartCoroutine(toSpwanTrue());
        }
    }

    IEnumerator toSpwanTrue()
    {
        yield return new WaitForSeconds(0.2f);
        toSpwan = true;

    }
}
