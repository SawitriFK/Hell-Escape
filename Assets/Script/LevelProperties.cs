using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperties : MonoBehaviour
{
    public GameObject portal;
    private GameManager GM;
    // Start is called before the first frame update
    void Awake()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        GM.ThisIsPortal(portal);
    }
}
