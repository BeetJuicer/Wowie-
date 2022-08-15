using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCameraManager : MonoBehaviour
{

    [SerializeField]
    GameObject virtualCam;

    [SerializeField]
    private GameObject cat;

    private GameObject ghostPlayer;

    private void Start()
    {
        ghostPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            virtualCam.SetActive(true);
            ghostPlayer.transform.position = cat.transform.position;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            virtualCam.SetActive(false);
        }
    }

}
