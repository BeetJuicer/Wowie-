using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRoom : MonoBehaviour
{
    [SerializeField]
    private string audioClipName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //play music
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.Play("Wipe");
            AudioManager.instance.Play(audioClipName);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //stop music
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.Play("Wipe_Out");
            AudioManager.instance.Stop(audioClipName);
        }
    }
}
