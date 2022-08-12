using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFollower : MonoBehaviour
{
    [SerializeField] private GameObject iceCreamCam;
    [SerializeField] private GameObject cakeCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isCake)
        {
            cakeCam.SetActive(true);
            iceCreamCam.SetActive(false);
        }
        else
        {
            cakeCam.SetActive(false);
            iceCreamCam.SetActive(true);
        }
    }
}
