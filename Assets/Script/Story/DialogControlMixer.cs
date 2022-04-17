using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;


public class DialogControlMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData){
        TextMeshProUGUI text = playerData as TextMeshProUGUI;
        string currentText = "";
        float currentAlpha = 0f;
        


        if(!text) {return;}

        int inputCount = playable.GetInputCount();
        for(int i = 0; i < inputCount; i++){
            float inputWeight = playable.GetInputWeight(i);
            if(inputWeight > 0f)
            {
                ScriptPlayable<DialogControlBehaviour> inputPlayable = (ScriptPlayable<DialogControlBehaviour>)playable.GetInput(i);
                DialogControlBehaviour input = inputPlayable.GetBehaviour();
                currentText = input.dialogText;
                currentAlpha = inputWeight;

            }
        }

        text.text = currentText;
        text.color = new Color(1,1,1,currentAlpha);


    }


    void Update(){

    }
    // private GameObject dialog;

    

    // public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    // {
    //     dialog = playerData as GameObject;

    //     if(dialog == null)
    //         return;
        
    //     int inputCount = playable.GetInputCount();

    //     // TextMeshProUGUI textComponent = TextMeshProUGUI.empty;
    //     string lines;
    //     float textSpeed= 0f;
    //     int index;

    //     for(int i = 0; i < inputCount; i++)
    //     {
    //         float inputWeight = playable.GetInputWeight(i);
    //         ScriptPlayable<DialogControlBehaviour> inputPlayable = (ScriptPlayable<DialogControlBehaviour>)playable.GetInput(i);
    //         DialogControlBehaviour behaviour = inputPlayable.GetBehaviour();
    //     }
    // }
}
