using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    private PlayableDirector timeline;

    private bool cutscenePaused;
    // Start is called before the first frame update
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cutscenePaused && PlayerInputHandler.GetInstance().GetSubmitPressed())
        {
            timeline.Unfreeze();
            cutscenePaused = false;
        }
    }

    public void CutscenePause()
    { 
        timeline.Freeze();
        cutscenePaused = true;
    }

    public void CutsceneFastForward()
    {
        timeline.FastForward();
    }
}
