using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Schmeckle : MonoBehaviour
{
    [SerializeField] GameObject pickUpEffect;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(transform.position.y + .2f, .5f).SetEase(Ease.InOutQuart).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(pickUpEffect);
            gameObject.SetActive(false);
        }
    }
}
