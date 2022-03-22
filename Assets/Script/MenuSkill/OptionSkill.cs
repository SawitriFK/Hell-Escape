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
        public int Mana;
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
/*    [SerializeField]
    public string[] skillPlayer = new string[3];*/

    public List<String> logy = new List<String>();
    public static List<string> log = new List<string>();
    int logyCount = 2;


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
    public Mana manaSkill;

    private void Awake()
    {
        fireballSkill = GameObject.FindObjectOfType<FireballSkill>();
    }

    private void Start()
    {
        generateSkill();

        if(log.Count > 0)
        {
            logy = log;
        }

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            /*            for (int a = 0; a < skillPlayer.Length; a++)
                        {
                            if(skillPlayer[a] == "")
                            {
                                Debug.Log("Ada Kosong = " + a);
                            }
                            else
                            {
                                Debug.Log("Tidak Kosng = " + a);
                            }
                        }*/

            if (log.Count == 0)
            {
                Debug.Log(" Kosong = ");
            }
            else
            {
                Debug.Log("Tidak Kosng = " );
            }

            if (logy.Count == 0)
            {
                Debug.Log("Y Ada Kosong = ");
            }
            else
            {
                Debug.Log("Y Tidak Kosng = ");
            }
        }

        if (Input.GetKey(KeyCode.O))
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
                //Debug.Log(indTS);
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
/*            for (int i = 0; i < skillPlayer.Length; i++)
            {
                if (skillPlayer[i] == "")
                {
                    skillPlayer[i] = tempSkill[opsi];
                    break;
                }
            }*/

            if (logy.Count >= logyCount)
            {
                logy.RemoveAt(0);
                logy.Add(tempSkill[opsi]);
                log.Add(tempSkill[opsi]);
            }
            else
            {
                logy.Add(tempSkill[opsi]);
                log.Add(tempSkill[opsi]);
            }
        }
        if(allSkill[findSkill(tempSkill[opsi])].typePasif == true)
        {
            switchSkill(tempSkill[opsi]);
        }
/*       Melihat List
 *       for (int a = 0; a < skillPlayer.Length; a++)
        {
            Debug.Log("adalah " + skillPlayer[a] + " uhu");
        }*/
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
        /*        int lengthSP = checkArrayLength(skillPlayer);
                //Debug.Log(lengthSP);
                if (lengthSP > 0)
                {
                    for (int i = 0; i < lengthSP; i++)
                    {
                        skillUIPlayer[i].sprite = allSkill[findSkill(skillPlayer[i])].Icon;
                    }
                }*/

        int lengthSP = logy.Count;
        //Debug.Log(lengthSP);
        if (lengthSP > 0)
        {
            for (int i = 0; i < lengthSP; i++)
            {
                skillUIPlayer[i].sprite = allSkill[findSkill(logy[i])].Icon;
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
        /*        for (int i = 0; i < skillPlayer.Length; i++)
                {
                    //Apa pilihan opsi terdapat pada penyimpanan skill?
                    if (cek == skillPlayer[i])
                    {
                        _checkPlayerSkill = true;
                        break;
                    }
                }*/
        
        for (int i = 0; i < logy.Count; i++)
        {
            //Apa pilihan opsi terdapat pada penyimpanan skill?
            if (cek == logy[i])
            {
                _checkPlayerSkill = true;
                break;
            }
        }
        return _checkPlayerSkill;
    }

    public void playerSkill(int skill)
    {
        /*swithSkillActive(skillPlayer[skill]);*/
/*        swithSkillActive(logy[skill]);*/
        Debug.Log(allSkill[findSkill(logy[skill])].Mana);
        bool activeMana = manaSkill.GetComponent<Mana>().UseSkill(allSkill[findSkill(logy[skill])].Mana);
        if(activeMana == true)
        {
            swithSkillActive(logy[skill]);
        }
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

