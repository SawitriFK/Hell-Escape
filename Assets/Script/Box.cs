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

    private Animator anim;

    int clik = 0;
    public float sec = 5f;

    public static bool boxSkill;

    void Start()
    {
        anim = GetComponent<Animator>();
        isEnter = false;
        text = GameObject.Find("UICanvas").GetComponent<UIProperties>().getBoxHint();
        MenuSkill = GameObject.Find("UICanvas").GetComponent<UIProperties>().getMenuSkillHint();
        boxSkill = false;
        //box = GameObject.Find("UICanvas").GetComponent<UIProperties>().getBoxHint();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            // text.SetActive(true);
            // isEnter = true;
            if (clik < 1)
                {
                    // anim.SetTrigger("openBox");
                    // MenuSkill.SetActive(true);
                    // clik++;
                    StartCoroutine(Wait());
                    text.SetActive(false);
                }


        }
    }

    // private void OnTriggerExit2D (Collider2D collision)
    // {
    //     if(collision.gameObject.tag == "player")
    //     {
    //         text.SetActive(false);
    //         isEnter = false;
    //     }
    // }

    // private async void FixedUpdate()
    // {
    //     // if (isEnter)
    //     // {
    //     //     if (Input.GetKeyDown(KeyCode.E) && clik < 1)
    //     //     {
    //     //         // anim.SetTrigger("openBox");
    //     //         // MenuSkill.SetActive(true);
    //     //         // clik++;
    //     //         StartCoroutine(Wait());
    //     //     }
    //     // }
    // }

    private IEnumerator Wait()
        {
            clik++;
            anim.SetTrigger("openBox");
            yield return new WaitForSeconds( 1.25f );
            Time.timeScale = 0.0f;
            MenuSkill.SetActive(true);
            boxSkill = true;

        }




}
