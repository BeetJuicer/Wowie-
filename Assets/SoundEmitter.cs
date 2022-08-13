using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SoundEmitter : MonoBehaviour
{
    [SerializeField] Enemy1 cat;
    private bool isPlayerDetected;

    private void Update()
    {
        if (isPlayerDetected)
        {
            if (PlayerInputHandler.GetInstance().GetInteractPressed())
            {
                Ring();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerDetected = false;
        }
    }

    private void Ring()
    {
        Debug.Log("Bell has been pressed");
        cat.Scare(transform);
    }
}
