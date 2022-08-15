using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject stopper;
    [SerializeField] private SpriteRenderer fallerSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stopper.SetActive(false);
            fallerSprite.color = Color.white;
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
