using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;

public class TypewriterEffectPlayableAsset : PlayableAsset, IPropertyPreview
{
    /*public string Text;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TypewriterEffectBehaviour>.Create(graph);

        TypewriterEffectBehaviour typewriterEffectBehaviour = playable.GetBehaviour();
        typewriterEffectBehaviour.DefaultText = Text;

        return playable;
    }*/
    [TextArea(6,20)]
    public string Text = "";
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TypewriterEffectBehaviour>.Create(graph);
        playable.GetBehaviour().DefaultText = Text;
        return playable;
    }

    // this will put the text field into preview mode when editing, avoids constant dirtying the scene
    public void GatherProperties(PlayableDirector director, IPropertyCollector driver)
    {
        driver.AddFromName<TextMeshProUGUI>("DialogueText");
    }   
}
