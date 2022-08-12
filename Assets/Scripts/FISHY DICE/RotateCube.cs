using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(0f, 0f, 15f), 0.2f).SetEase(Ease.InOutFlash).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
