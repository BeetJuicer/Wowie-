using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    [SerializeField] Transform check;

    public void CheckForDoor()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(check.position, 0.5f);
        foreach (Collider2D detectedObject in detectedObjects)
        {
            if (detectedObject.CompareTag("Door"))
            {
                detectedObject.gameObject.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(check.position, 0.5f);
    }
}
