using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SimpleMovement : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    [SerializeField] private GameObject sprite;

    private int xInput;
    private int yInput;

    public float movementSpeed = 2f;

    public float xRotation;
    public float yRotation;

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

        if (xInput == 0 && yInput == 0) { sprite.transform.rotation = Quaternion.Euler(0f, 0f, 0f); }
        if (xInput == 1 && yInput == 0) { sprite.transform.rotation = Quaternion.Euler(0f, 0f, -90f); }
        if (xInput == -1 && yInput == 0) { sprite.transform.rotation = Quaternion.Euler(0f, 0f, 90f); }

        if (xInput == 0 && yInput == 1) { sprite.transform.rotation = Quaternion.Euler(0f, 0f, 0f); }
        if (xInput == 1 && yInput == 1) { sprite.transform.rotation = Quaternion.Euler(0f, 0f, -45f); }
        if (xInput == -1 && yInput == 1) { sprite.transform.rotation = Quaternion.Euler(0f, 0f, 45f); }

        if (xInput == 0 && yInput == -1) { sprite.transform.rotation = Quaternion.Euler(0f, 0f, 180f); }
        if (xInput == 1 && yInput == -1) { sprite.transform.rotation = Quaternion.Euler(0f, 0f, 225f); }
        if (xInput == -1 && yInput == -1) { sprite.transform.rotation = Quaternion.Euler(0f, 0f, 135f); }


        sprite.transform.rotation = Quaternion.Euler(0f, 0f, yRotation - xRotation);

        transform.Translate(new Vector2(xInput, yInput) * Time.deltaTime * movementSpeed);
    }
}
