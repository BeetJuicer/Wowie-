using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class TypewriterTrackMixer : PlayableBehaviour
{

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
            TextMeshProUGUI text = playerData as TextMeshProUGUI;
            string currentText = "";
            float currentAlpha = 0f;

        if (!text) { return; }

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++)
        {

            float inputWeight = playable.GetInputWeight(i);

            if (inputWeight > 0f)
            {
                ScriptPlayable<TypewriterEffectBehaviour> inputPlayable = (ScriptPlayable<TypewriterEffectBehaviour>)playable.GetInput(i);

                TypewriterEffectBehaviour input = inputPlayable.GetBehaviour();
                currentText = input.DefaultText;
                currentAlpha = inputWeight;
            }

        }

        text.color = new Color(1, 1, 1, currentAlpha);
        //text.text = currentText; //sets the text that was taken as the text to be displayed.

        // given the current time, determine how much of the string will be displayed
        var progress = (float)(playable.GetTime() / playable.GetDuration()); // Problem: always 0.
        var subStringLength = Mathf.RoundToInt(Mathf.Clamp01(progress) * currentText.Length);
        text.text = currentText.Substring(0, subStringLength);

    }
}
