using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class DialogueText : MonoBehaviour{
    public TextMeshProUGUI textComponent;
    public string lines;
    public float textSpeed;
    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartDialogue(){
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine(){
        foreach(char c in lines.ToCharArray()){
            textComponent.text  += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // void NextLine(){
    //     if(index < lines.Length - 1){
    //         index++;
    //         textComponent.text = string.Empty;
    //         StartCoroutine(TypeLine());
    //     }
    //     else{
    //         gameObject.SetActive(false);
    //     }
    // }

    // public void ButtonNext(){
    //     Debug.Log("Check");

    //     if(textComponent.text == lines[index]){
    //         NextLine();
    //         Debug.Log("Next");
    //     }
    //     else{
    //         StopAllCoroutines();
    //         textComponent.text = lines[index];
    //         Debug.Log("Stop");
    //     }
            

    // }
}
