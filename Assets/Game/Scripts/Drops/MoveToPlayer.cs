using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveToPlayer : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.DOLocalMove(player.transform.position, 0.7f).SetEase(Ease.InOutSine);
    }
}
