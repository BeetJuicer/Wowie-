using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(0f, 0f, 180f), 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Incremental);    
    }

    // Update is called once per frame
    void Update()
    {
    }
}
