using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PickUp : MonoBehaviour
{
    [SerializeField] GameObject pickUpEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(pickUpEffect, transform.position, Quaternion.identity);
            collision.GetComponent<SoulStats>().soulCount++;
            gameObject.SetActive(false);
        }
    }
}
