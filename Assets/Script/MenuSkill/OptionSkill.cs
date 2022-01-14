using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



public class OptionSkill : MonoBehaviour
{
    [Serializable]
    public struct skill
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        public bool typePasif;
    }

    [Serializable]
    public struct skillUI
    {
        public Text Name;
        public Text Description;
        public Image Icon;
    }

    public int currentSkill;

    [Header("Menu Skill")]
    [SerializeField] skill[] allSkill;
    [SerializeField] skillUI[] allSkillUI;
    [SerializeField] private GameObject menuSkill;

    // penampung opsi skill
    public string[] tempSkill = new string[3];

    //Pilihan untuk swith fungsi skill
    //public KindSkill kindSkill;

    //Skill Pilihan
    [SerializeField]
    public string[] skillPlayer = new string[3];

    // UI Skill Player
    public Image[] skillUIPlayer = new Image[3];

    [Header("Script Aktif")]
    //Fireball Skill Script
    public FireballSkill fireballSkill;
    // Cyclone Skill Script
    public CycloneSkill cycloneSkill;


    [Header("Script Pasif")]
    // Skill
    public Health healthForMax;
    public Curse curseForMin;
    public PlayerBattle battleForDamage;

    private void Awake()
    {
        fireballSkill = GameObject.FindObjectOfType<FireballSkill>();
    }

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

        playerSkillUI();
    }
    void generateSkill()
    {

        allSkill.Shuffle(allSkill.Length);

        int indTS = 0;
        for (int z = 0; z < allSkill.Length; z++)
        {
            
            if(checkPlayerSkill(allSkill[z].Name) == false && indTS < 3)
            {
                Debug.Log(indTS);
                tempSkill[indTS] = allSkill[z].Name;

                allSkillUI[indTS].Name.text = allSkill[findSkill(tempSkill[indTS])].Name;
                allSkillUI[indTS].Description.text = allSkill[findSkill(tempSkill[indTS])].Description;
                allSkillUI[indTS].Icon.sprite = allSkill[findSkill(tempSkill[indTS])].Icon;
                indTS++;
            }
        }

        


    }

    public void option(int opsi)
    {
        if (allSkill[findSkill(tempSkill[opsi])].typePasif == false && checkPlayerSkill(tempSkill[opsi]) == false)
        {
            for (int i = 0; i < skillPlayer.Length; i++)
            {
                if (skillPlayer[i] == "")
                {
                    skillPlayer[i] = tempSkill[opsi];
                    break;
                }
            }
        }
        switchSkill(tempSkill[opsi]);
        menuSkill.SetActive(false);
        Time.timeScale = 1;
        generateSkill();

    }


    public int findSkill(string indexTemp)
    {
        int findIndex = -1;
        for (int a = 0; a < allSkill.Length; a++)
        {
            if (allSkill[a].Name == indexTemp)
            {

                findIndex = a;
                break;
            }
        }
        return findIndex;
    }

    public void switchSkill(string skill)
    {
        switch (skill)
        {
            case "Health Max":
                healtMax();
                break;
            case "Curse Reduction":
                curseMin();
                break;
            case "Damage ++":
                plusDamage();
                break;
            /*            case "Skill 2":
                            kindSkill.speedMax();
                            break;
                        case "Skill 3":
                            kindSkill.jumpMax();
                            break;*/
            default:
                healtMax();
                break;
        }
    }

    public void playerSkillUI()
    {
        int lengthSP = checkArrayLength(skillPlayer);
        Debug.Log(lengthSP);
        if (lengthSP > 0)
        {
            for (int i = 0; i < lengthSP; i++)
            {
                skillUIPlayer[i].sprite = allSkill[findSkill(skillPlayer[i])].Icon;
            }
        }

    }

    public int checkArrayLength(string[] _array)
    {
        int lenght = 0;
        for(int i = 0; i < _array.Length; i++)
        {

            if(_array[i] == "")
            {
                lenght = i;
                break;
            }
            else
            {
                lenght = _array.Length;

            }

            
        }
        return lenght;
    }



    public bool checkPlayerSkill(string cek)
    {
        bool _checkPlayerSkill = false;
        for (int i = 0; i < skillPlayer.Length; i++)
        {
            //Apa pilihan opsi terdapat pada penyimpanan skill?
            if (cek == skillPlayer[i])
            {
                _checkPlayerSkill = true;
                break;
            }
        }
        return _checkPlayerSkill;
    }

    public void playerSkill(int skill)
    {
        swithSkillActive(skillPlayer[skill]);

        /*        for (int i = 0; i < allSkill.Length; i++)
                {
                    if (skillPlayer[skill] == allSkill[].Name) ;
                    {

                    }
                }*/
    }

    public void swithSkillActive(string skill)
    {
        switch (skill)
        {
            case "Fireball":
                StartCoroutine(fireballSkill.Shoot());
                break;
            case "Cyclone":
                StartCoroutine(cycloneSkill.Spin());
                break;
        }
    }



    // Skill
    public void healtMax()
    {
        
        healthForMax.GetComponent<Health>().addHealth(500);
    }
    public void curseMin()
    {

        curseForMin.TakeCurseDamage(-200);
    }
    public void plusDamage()
    {

        battleForDamage.damage = battleForDamage.damage + 20;
        battleForDamage.damage2 = battleForDamage.damage2 + 20;
    }
}

//SHUFFEL / RANDOM
public static class ShuffleExtension
{
    //shuffle arrays:
    public static void Shuffle<T>(this T[] array, int shuffleAccuracy)
    {
        for (int i = 0; i < shuffleAccuracy; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, array.Length);

            T temp = array[randomIndex];
            array[randomIndex] = array[0];
            array[0] = temp;
        }
    }
    //shuffle lists:
    public static void Shuffle<T>(this List<T> list, int shuffleAccuracy)
    {
        for (int i = 0; i < shuffleAccuracy; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, list.Count);

            T temp = list[randomIndex];
            list[randomIndex] = list[0];
            list[0] = temp;
        }
    }
}

