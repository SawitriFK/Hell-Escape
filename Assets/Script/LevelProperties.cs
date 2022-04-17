using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperties : MonoBehaviour
{
    public GameObject portal;
    public GameObject box;

    private GameManager GM;
    private Curse CR;
    // Start is called before the first frame update
    void Awake()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        CR = GameObject.Find("Player").GetComponent<Curse>();
        GM.ThisIsPortal(portal);
        CR.ThisIsBox(box);

    }
}
