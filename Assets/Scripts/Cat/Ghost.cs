using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    [SerializeField]
    private Cat cat;
    [SerializeField]
    private Transform targetLocation;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        targetLocation.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.GetWhisperPressed())
        {
            targetLocation.gameObject.SetActive(false);
            targetLocation.position = transform.position;
            targetLocation.gameObject.SetActive(true);
            cat.Call(targetLocation);
        }

        DebugPanel.Log("distance of target to cat", cat.gameObject.transform.position.x - targetLocation.position.x);

        if (Mathf.Abs(cat.gameObject.transform.position.x - targetLocation.position.x) < 0.5f)
        {
            targetLocation.gameObject.SetActive(false);
        }
    }
}
