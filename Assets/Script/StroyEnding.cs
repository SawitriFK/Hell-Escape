using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StroyEnding : MonoBehaviour
{
    Animator anim;
    private float coolDownTimer = 0.0f;
    public float cooldown = 3f;
    private bool thankyou = false;
    private bool seeyou = false;
    private bool sometime = false;
    private bool fadeout = false;
    public PlayableDirector pb;
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
        Debug.Log("Masuik Start cour");
        if (pb.GetComponent<PlayableDirector>().state != PlayState.Playing)
        {
            Debug.Log("Sudah berhenti");
            yield return new WaitForSeconds(2);
            anim.SetTrigger("fadeout");
        }
    }

    void Update(){
        // StartCoroutine(StartScene(cooldown));
        if (pb.GetComponent<PlayableDirector>().state != PlayState.Playing)
        {
            anim.SetTrigger("fadeout");
        }
    }

}
