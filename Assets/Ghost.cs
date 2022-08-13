using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    [SerializeField]
    private Enemy1 enemy;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.GetWhisperPressed())
        {
            Debug.Log("Whisper pressed.");
            enemy.Call();
        }
    }
}
