using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTracker : MonoBehaviour
{
    //[SerializeField] private GameObject timeIndicator; - UI object to disable

    private static CutsceneTracker instance;

    public bool cutsceneIsPlaying { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Cutscene Tracker in the scene");
        }
        instance = this;
    }

    public static CutsceneTracker GetInstance()
    {
        return instance;
    }

    public void StartCutscene()
    {
        cutsceneIsPlaying = true;
      //  timeIndicator.SetActive(false);
    }
    
    public void StopCutscene()
    {
        cutsceneIsPlaying = false; 
     //   timeIndicator.SetActive(true);
    } 


}
