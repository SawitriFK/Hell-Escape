using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// public class DialogControlClip : PlayableAsset, ITimelineClipAsset
public class DialogControlClip : PlayableAsset
{
    // [SerializeField] private DialogControlBehaviour template = new DialogControlBehaviour();
    // public ClipCaps clipCaps
    // {get{return ClipCaps.Blending;}}

    // public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    // {
    //     return ScriptPlayable<DialogControlBehaviour>.Create(graph, template);
    // }

    public string dialogText;
    

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogControlBehaviour>.Create(graph);

        DialogControlBehaviour behaviour = playable.GetBehaviour();
        behaviour.dialogText = dialogText;

        return playable;
    }
}
