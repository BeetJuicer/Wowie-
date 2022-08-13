using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveTo(1);
    }

    public void MoveTo(int direction)
    {
        rb.AddForce(Vector2.right * movementSpeed * direction);
    }
}
