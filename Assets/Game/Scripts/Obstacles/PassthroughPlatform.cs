using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class PassthroughPlatform : MonoBehaviour
{
    private BoxCollider2D m_BoxCollider2D;
    private Collider2D playerCollider;

    private bool inContactWithPlayer;
    private void Awake()
    {
        m_BoxCollider2D = GetComponent<BoxCollider2D>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
    }


    private void Update()
    {
        if (inContactWithPlayer)
        {
            if (PlayerInputHandler.GetInstance().NormInputY < 0)
            {
                Timing.RunCoroutine(DisableCollision());
            }
        }
    }
    IEnumerator<float> DisableCollision()
    {
        Physics2D.IgnoreCollision(playerCollider, m_BoxCollider2D);
        yield return Timing.WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, m_BoxCollider2D, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inContactWithPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inContactWithPlayer = false;
        }
    }

}
