using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    private PlayableDirector timeline;

    private bool hasPlayedWitchCutscene;
    private bool hasPlayedOutro;

    [SerializeField]
    private int cutsceneNumber;
    // Use this for initialization
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
        hasPlayedWitchCutscene = PlayerPrefs.GetInt("hasPlayedWitchCutscene") == 1;
        hasPlayedOutro = PlayerPrefs.GetInt("hasPlayedOutro") == 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (cutsceneNumber == 0 && !hasPlayedWitchCutscene || cutsceneNumber == 1 && !hasPlayedOutro)
            {
                timeline.Play();
            }
        }
    }

    
    public void CutsceneHasPlayed()
    {
        if(cutsceneNumber == 0)
        {
        hasPlayedWitchCutscene = true;
        PlayerPrefs.SetInt("hasPlayedWitchCutscene", 1);
        }
        if(cutsceneNumber == 1)
        {
        hasPlayedOutro = true;
        PlayerPrefs.SetInt("hasPlayedOutro", 1);
        }
    }

}
