using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    Animator anim;
    private float coolDownTimer = 0.0f;
    public float cooldown = 3f;
    private bool thankyou = false;
    private bool seeyou = false;
    private bool sometime = false;
    private bool fadeout = false;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetTrigger("fadein");
    }

    void Start()
    {
        StartCoroutine(StartScene(cooldown));
    }

    IEnumerator StartScene(float seconds)
    {
        yield return new WaitForSeconds(10);
        anim.SetTrigger("fadeout");
        yield return new WaitForSeconds(seconds);
        anim.SetTrigger("seeyou");
        yield return new WaitForSeconds(seconds);
        anim.SetTrigger("sometime");
    }

    // void Update()
    // {
    //     coolDownTimer += Time.deltaTime;
    //     if(coolDownTimer >= cooldown)
    //     {
    //         if(!thankyou)
    //         {
    //             anim.SetTrigger("thankyou");
    //             thankyou = true;
    //         }else if(!fadeout)
    //         {
    //             anim.SetTrigger("fadeout");
    //             fadeout = true;
    //         }else if(!seeyou)
    //         {
    //             anim.SetTrigger("seeyou");
    //             seeyou = true;
    //         }else if(!sometime)
    //         {
    //             anim.SetTrigger("sometime");
    //             sometime = true;
    //         }
    //     }
        
    // }
}
