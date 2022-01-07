using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player;
    private GameManager GM;
    public bool lastSpawnPoint;
    private bool touched = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(!touched){
            touched = true;
            gameObject.SetActive(false);
            GM.Spawn(transform);
            if(lastSpawnPoint)
            {
                GM.allEnemySpawned();
            }
        }
    }


}
