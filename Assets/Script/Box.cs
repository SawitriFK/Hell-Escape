using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool isEnter;
    private GameObject text;
    private GameObject OptionSkill;
    //private GameObject menuSkill;

    private GameObject MenuSkill;
    public GameObject box;

    int clik = 0;
    public float sec = 5f;

    void Start()
    {
        isEnter = false;
        text = GameObject.Find("UICanvas").GetComponent<UIProperties>().getBoxHint();
        MenuSkill = GameObject.Find("UICanvas").GetComponent<UIProperties>().getMenuSkillHint();
        //box = GameObject.Find("UICanvas").GetComponent<UIProperties>().getBoxHint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            text.SetActive(true);
            isEnter = true;


        }
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            text.SetActive(false);
            isEnter = false;
        }
    }

    private void FixedUpdate()
    {
        if (isEnter)
        {
            if (Input.GetKeyDown(KeyCode.E) && clik < 1)
            {
                MenuSkill.SetActive(true);
                clik++;
            }
        }
    }


}
