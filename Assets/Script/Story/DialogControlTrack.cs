using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;

[TrackColor(241f/255f, 249f/255f, 99f/255f)]
[TrackBindingType(typeof(TextMeshProUGUI))]
[TrackClipType(typeof(DialogControlClip))]
public class DialogControlTrack : TrackAsset{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<DialogControlMixer>.Create(graph, inputCount);
    }

}


