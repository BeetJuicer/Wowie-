using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveX(-25.12f, 6f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
