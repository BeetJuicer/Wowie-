using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderCopier : MonoBehaviour
{
    private PolygonCollider2D camCollider;
    private PolygonCollider2D ghostCollider;

    private void Start()
    {
        ghostCollider = GetComponent<PolygonCollider2D>();
        PolygonCollider2D[] camColliders = GetComponentsInParent<PolygonCollider2D>();

        foreach (PolygonCollider2D collider in camColliders)
        {
            if (collider.gameObject.name == "Room")
            {
                camCollider = collider;
            }
        }
    }

    private void Update()
    {
        Vector2[] points = camCollider.points;
        ghostCollider.points = points;
    }
}
