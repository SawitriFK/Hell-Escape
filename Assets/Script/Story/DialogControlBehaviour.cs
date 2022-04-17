using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class DialogControlBehaviour : PlayableBehaviour
{
    public string dialogText;



    // public TextMeshProUGUI textComponent;
    // public string lines;
    // public float textSpeed;
    // private int index;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI text = playerData as TextMeshProUGUI;
        text.text = dialogText;
       
       
        
        
        

        // foreach(char c in dialogText.ToCharArray()){
        //     text.text  += c;
        //     // yield return new WaitForSeconds(textSpeed);
        // }
        text.color = new Color(1,1,1, info.weight);
    }

}
