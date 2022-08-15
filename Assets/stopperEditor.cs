using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class stopperEditor : MonoBehaviour
{
    private BoxCollider2D Bcollider;
    [SerializeField] private BoxCollider2D fallerCollider;

    // Start is called before the first frame update
    void Start()
    {
        Bcollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Bcollider.size = new Vector2(fallerCollider.size.x, 0.01f);
        Bcollider.transform.position = new Vector2(fallerCollider.transform.position.x ,
            (fallerCollider.transform.position.y - .5f)
            - fallerCollider.bounds.extents.y);
    }
}
