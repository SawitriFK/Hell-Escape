using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollHowtoplay : MonoBehaviour
{
    public GameObject scrollbar;
    float scrol_pos = 0;
    float [] pos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length-1f);
        for (int i = 0; i < pos.Length; i++){
            pos[i] = distance * i;
        }

        if(Input.GetMouseButton(0)){
            scrol_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else{
            for(int i = 0; i < pos.Length; i ++){
                if(scrol_pos < pos[i] + (distance/2) && scrol_pos > pos[i] - (distance/2)){
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.15f);
                }
            }
        }
    }
}
