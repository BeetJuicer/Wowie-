using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class projectile : MonoBehaviour
{
    private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = CannonManager.GetInstance().projectileSpeed;
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.GetComponent<PlayerHandler>().isDead)
        {
            collision.gameObject.transform.DOShakePosition(.16f, .2f);
            collision.GetComponent<PlayerHandler>().isDead = true;
        }

        if (collision.CompareTag("Bounds"))
        {
            Destroy(gameObject);//replace with trashman object pooling
        }
    }

}
