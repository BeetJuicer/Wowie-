using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Bell : MonoBehaviour
{
    [SerializeField] Cat cat;
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
        cat.Scare(transform);
    }
}
