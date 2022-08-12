using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCameraManager : MonoBehaviour
{

    [SerializeField]
    GameObject virtualCam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") &&  !other.isTrigger)
        {
            virtualCam.SetActive(true);
           // Debug.Log("Player has activated: " + gameObject.name);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") &&  !other.isTrigger)
        {
            virtualCam.SetActive(false);
            //Debug.Log("Player has deactivated: " + gameObject.name);
        }
    }

}
