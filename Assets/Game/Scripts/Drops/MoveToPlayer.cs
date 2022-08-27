using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveToPlayer : MonoBehaviour
{
    private GameObject player;

    private float minModifier = 13;
    private float maxModifier = 17;

    Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));
    }
}
