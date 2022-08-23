using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderEditor : MonoBehaviour
{
    private BoxCollider2D Bcollider;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Bcollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Bcollider.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
    }
}
