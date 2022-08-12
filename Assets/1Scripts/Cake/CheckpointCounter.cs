using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCounter : MonoBehaviour
{
    [SerializeField] private int pointNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pointNumber > GameManager.GetInstance().checkPointCount)
        {
            if (collision.CompareTag("Player"))
            {
                GameManager.GetInstance().checkPointCount++;
                AudioManager.instance.Play("coin"); 
            }
        }
    }
}
