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
    // public static string manaString = "Hail";


    private float calculateLife;
    void Start()
    {
        // Debug.Log("Mana = "+ playerMana);
        // // Debug.Log("Current Mana = "+ currentMana);
        // Debug.Log("String Mana= "+ manaString);
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

    void FixedUpdate()
    {
        if(playerMana != -1){
            if( currentMana < maxMana)
            {
                currentMana += 1 * Time.deltaTime;
                playerMana = currentMana;
                manaBar.SetValue(currentMana);
            }
            if(currentMana > maxMana)
            {
                currentMana = maxMana;
                playerMana = currentMana;
                manaBar.SetValue(currentMana);
            }
        }
    }

    public bool UseSkill(float _manaSkill)
    {

        if(currentMana > _manaSkill)
        {
            currentMana -= _manaSkill;
            playerMana = currentMana;
            manaBar.SetValue(currentMana);
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void manaToMax(){
            currentMana = maxMana;
            playerMana = maxMana;
            manaBar.SetValue(currentMana);
    }
    // Update is called once per frame
}
