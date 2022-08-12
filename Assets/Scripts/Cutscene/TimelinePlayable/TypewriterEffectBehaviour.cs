using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class TypewriterEffectBehaviour : PlayableBehaviour
{
     
    public string DefaultText;
    public string inputText;
    public string test = "Hey! I'm </color=\"red\">John</color>, my name is in red.";

    private PlayableDirector mDirector;

    // called every frame the clip is active
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        mDirector = playable.GetGraph().GetResolver() as PlayableDirector;

        var textObject = playerData as TextMeshProUGUI;
        if (textObject == null)
            return;

        // given the current time, determine how much of the string will be displayed
        var progress = (float)(playable.GetTime() / playable.GetDuration());
        var subStringLength = Mathf.RoundToInt(Mathf.Clamp01(progress) * GetTextLength(DefaultText));

        inputText = DefaultText + " ";

        if (PlayerInputHandler.GetInstance().GetSubmitPressed())
        {
            mDirector.FastForward();
        }

        string myString = MySubstring(inputText, subStringLength);
        textObject.text = myString;
    }

    private string MySubstring(string input, int endPoint)
    {
        bool richText = false;
        char[] inputLine = input.ToCharArray();
        string outputText = "";
        int finalCount = -1;

        for (int i = 0; i < input.Length; i++)
        {
            if(finalCount == endPoint)
            {
                outputText = input.Substring(0, i);
            }

            if (inputLine[i] == '<' || richText)
            {
                richText = true;
                if(inputLine[i] == '>')
                {
                    richText = false;
                }
            }
            else
            {
                finalCount++;
            }     
        }
        return outputText;
    }

    private int GetTextLength(string input)
    {
        bool richText = false;
        char[] inputLine = input.ToCharArray();
        int textLength = -1;

        for (int i = 0; i < input.Length; i++)
        {
            if (inputLine[i] == '<' || richText)
            {
                richText = true;
                if (inputLine[i] == '>')
                {
                    richText = false;
                }
            }
            else
            {
                textLength++;
            }
        }

        return textLength;
    }
}
