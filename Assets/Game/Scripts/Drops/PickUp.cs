using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PickUp : MonoBehaviour
{
    [SerializeField] GameObject pickUpEffect;

    void Start()
    {
        // Move up and down.
        transform.DOMoveY(transform.position.y + .2f, .5f).SetEase(Ease.InOutQuart).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(pickUpEffect);
            gameObject.SetActive(false);
        }
    }
}
