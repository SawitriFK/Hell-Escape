using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private bool isEnter;
    private GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        isEnter = false; 
        text = GameObject.Find("UICanvas").GetComponent<UIProperties>().getPortalHint();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
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
        if(isEnter)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                GameManager.level++;
                if(GameManager.level <= GameManager.maxLevel)
                {
                    
                    SceneManager.LoadScene (SceneManager.GetActiveScene().name);
                }
                else
                {
                    SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
                    
                }
                
            }
        }
    }
}
