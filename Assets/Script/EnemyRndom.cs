using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRndom : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int enemy;


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
/*
        //int temp = -1;
        int i = 0;
        int[] dis = new int[spawnPoints.Length];


        for(int k = 0; k < spawnPoints.Length; k++)
        {
            dis[k] = -1;
            //Debug.Log(dis[k]);
        }

        *//*        Debug.Log("Ini dis" + dis[0]);*/
/*        foreach (var item in dis)
        {
            Debug.Log(item);
        }*//*



        while (i < enemy)
        {

            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            bool cek = false;
            
            Debug.Log(i);

            int j=0;

            

            Debug.Log("Nilai I " + dis[i]);
            Debug.Log("Nilai Random " + randSpawnPoint);
            
            if(randSpawnPoint != dis[i])
            {
                Debug.Log("AHA");
                dis[i] = randSpawnPoint;
                
            }
            Debug.Log("Nilai I " + dis[i]);
            i++;
            
*//*            for (int j = 0; j < i+1; j++)
            {
                Debug.Log("Nilai array " + dis[j]);
                Debug.Log("Random " + randSpawnPoint);
                if (randSpawnPoint != dis[j])
                {
                    cek = true;
                    Debug.Log("BEDAAAAAAAAAAAAAAAAAAAAAA");
                }
                else
                {
                    cek = false;
                    Debug.Log("SAAAMAAAAA");
                }
                
            }*/

/*            if (cek == true)
            {
                Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
                dis[i] = randSpawnPoint;
                Debug.Log("Nilai araay i " + dis[i]);
                i++;
            }*/
/*                dis[i] = randSpawnPoint;
                Debug.Log("Nilai araay i " + dis[i]);*/
            /*
                        if (randSpawnPoint != dis)
                        {
                            Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
                            temp = randSpawnPoint;
                            i++;
                        }*/


            /*            if (randSpawnPoint != temp)
                        {
                            Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
                            temp = randSpawnPoint;
                            i++;
                        }
            */



            /*            Debug.Log(temp);
                        Debug.Log(randSpawnPoint);*/

            
        }



    // Update is called once per frame
    void Update()
    {

    }
}
