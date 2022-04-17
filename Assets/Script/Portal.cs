using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private bool isEnter;
    private GameObject text;
    

    // public Box box;

    // Start is called before the first frame update
    void Start()
    {
        isEnter = false; 
        text = GameObject.Find("UICanvas").GetComponent<UIProperties>().getPortalHint();
        // box = GetComponent<Box>();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            // text.SetActive(true);
            // isEnter = true;
            // if(Box.boxSkill == true){
            //     GameObject.Find("UICanvas").GetComponent<Animator>().SetTrigger("fadeout");
            // }

            GameObject.Find("UICanvas").GetComponent<Animator>().SetTrigger("fadeout");

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

    // private void FixedUpdate()
    // {
    //     if(isEnter)
    //     {
    //         if(Input.GetKeyDown(KeyCode.E))
    //         {
    //             GameObject.Find("UICanvas").GetComponent<Animator>().SetTrigger("fadeout");
    //         }
    //     }
    // }
}
