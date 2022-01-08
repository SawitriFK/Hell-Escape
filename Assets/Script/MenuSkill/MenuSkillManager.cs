using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSkillManager : MonoBehaviour
{   
    public int currentSkill;
    
    [Header("Menu Skill")]
    public List<Skills> skill;
    public List<SkillsUI> skillUI;
    [SerializeField ]private GameObject menuSkill;

    public int[] tempSkill = new int[3];
    bool optionArray = true;
    public int[] skillPlayer = new int[4];

    [Header("Skill Player")]
    public Text[] skillUIPlayer = new Text[4];

    [Header("Skill Player")]
    [SerializeField] public Button button1, button2, button3;
    bool isClicked = false;


    private void Start()
    {
        generateSkill();
    }

    private void Update()
    {


        if (Input.GetKey(KeyCode.P))
        {
            menuSkill.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    void generateSkill()
    {
        int i = 0;
        while (i < skillUI.Count)
        {
            currentSkill = Random.Range(0, skill.Count);

            for (int j = 0; j < 3; j++)
            {
                if (tempSkill[j] != currentSkill)
                {
                    optionArray = false;
                }
                else
                {
                    optionArray = true;
                    break;
                }
            }


            if (optionArray == false)
            {
                skillUI[i].nameSkillTxt.text = skill[currentSkill].nameSkill;
                skillUI[i].desSkillTxt.text = skill[currentSkill].desSkill;
                tempSkill[i] = currentSkill;
                i++;
            }
            optionArray = true;


        }


    }



    public void selectSkill(int index)
    {
        
        int i = 0;
        
        bool con = false;

        while (i < 4)
        {
            for (int j = 0; j < 4; j++)
            {
                
                if (skillPlayer[j] == tempSkill[index] )
                {
                    con = true;
                    break;
                }

            }
            Debug.Log(con);
            if (con == false && skillPlayer[i] == -1)
            {
                skillPlayer[i] = tempSkill[index];
                skillUIPlayer[i].text = skill[skillPlayer[i]].nameSkill;
                generateSkill();
                menuSkill.SetActive(false);
                Time.timeScale = 1;
                break;

            }
            i++;
            con = false;
        }
        
    }


}
