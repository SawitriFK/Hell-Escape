using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public Bar manaBar;
    public float maxMana = 100;
    [SerializeField] private float currentMana;
    public static float playerMana = -1;
    public static float playerMaxMana = -1;
    void Start()
    {
        if (playerMaxMana == -1)
        {
            playerMaxMana = maxMana;
        }
        else
        {
            maxMana = playerMaxMana;
        }

        manaBar.SetMaxValue(playerMaxMana);
        if (playerMana == -1)
        {
            currentMana = maxMana;
            playerMana = currentMana;
        }
        else
        {
            currentMana = playerMana;
            manaBar.SetValue(currentMana);
        }
    }

    public void UseSkill(float useMana)
    {
        float _maxMana = maxMana;
        bool emptyMana = false;
        currentMana -= useMana;

        if(currentMana > maxMana)
        {
            manaBar.SetValue(maxMana);
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
