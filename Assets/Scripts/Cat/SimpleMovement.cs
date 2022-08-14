using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private PlayerInputHandler inputHandler;

    private int xInput;
    private int yInput;

    public float movementSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = inputHandler.NormInputX;
        yInput = inputHandler.NormInputY;

        transform.Translate(new Vector2(xInput, yInput) * Time.deltaTime * movementSpeed);
    }
}
