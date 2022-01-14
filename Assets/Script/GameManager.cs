using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum CauseOfDeath {Health, Curse};
    [Header("Level Design")]
    public static int level = 1;
    public static int maxLevel = 6;
    public static bool playerDead = false;
    public static CauseOfDeath whyDead;
    public GameObject[] levelPrefab;
    public GameObject BossBar;

    public GameObject Player;
    private GameObject Portal;
    private GameObject Box;
    private Health playerHealth;
    private Curse playerCurse;

    [Header("Fall")]
    public float minHeightForDeath;
    public float fallDamage = 25f;
    public Transform SpawnPoint;

    [Header("Enemy")]
    [SerializeField] private GameObject[] enemyPrefab = null;
    public int enemyNum;
    private bool allSpawned = false;

    void Awake()
    {
        if(level == maxLevel)
        {
            BossBar.SetActive(true);
            int rand = levelPrefab.Length-1;
            Instantiate(levelPrefab[rand], levelPrefab[rand].transform.position, levelPrefab[rand].transform.rotation);
        }else
        {
            int rand = Random.Range(0,levelPrefab.Length-1);
            Instantiate(levelPrefab[rand], levelPrefab[rand].transform.position, levelPrefab[rand].transform.rotation);
        }
        GameObject.Find("UICanvas").GetComponent<Animator>().SetTrigger("fadein");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = Player.GetComponent<Health>();
        playerCurse = Player.GetComponent<Curse>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player == null)
        {
            return;
        }
        
        if(playerDead)
        {
            level = 1;
            StartCoroutine(Delay());
        }

        if(GameObject.FindGameObjectsWithTag("enemy").Length == 0 && allSpawned)
        {
            Portal.SetActive(true);
            Box.SetActive(true);
        }

        if(Player.transform.position.y < minHeightForDeath)
        {
            Player.transform.position = SpawnPoint.position;
            FindObjectOfType<AudioManager>().Play("PlayerFall");
            playerHealth.TakeDamage(fallDamage);
        }   
    }

    public void Spawn(Transform spawnPos)
    {
        int size = System.Convert.ToInt32(enemyPrefab.Length);
        int randEnemy = Random.Range(1,enemyNum);
        for (int i = 0; i < spawnPos.childCount; i++)
        {
            Transform child = spawnPos.GetChild(i).gameObject.transform;
            for(int j = 0; j < randEnemy; j++)
            {
                int spawnEnemy = Random.Range(0,size);
                Instantiate(enemyPrefab[spawnEnemy], child.position, child.rotation);
            }
        }
    }

    public void allEnemySpawned()
    {
        allSpawned = true;
    }

    IEnumerator Delay()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
        GameObject.Find("UICanvas").GetComponent<Animator>().SetTrigger("fadeout");
    }

    public void ThisIsPortal(GameObject portal)
    {
        Portal = portal;
    }
<<<<<<< HEAD

    public void ThisIsBox(GameObject box)
    {
        Box = box;
    }


=======
>>>>>>> 90aa404dc07faa969abd491c783e7f96901bdca2
}
