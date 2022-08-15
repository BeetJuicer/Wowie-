using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostConfiner : MonoBehaviour
{

    Vector3 lastPos;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lastPos = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Playe has exited");
            collision.transform.position = lastPos;
        }
    }
}
