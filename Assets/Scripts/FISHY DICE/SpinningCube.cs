using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpinningCube : MonoBehaviour
{
    public float rotationAmount = 45f;
    public float spinCooldown = 0.3f;
    public float spinDuration = 0.3f;

    private float elapsed;
    private float count;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Sequence().SetDelay(spinDuration).Append(transform.DORotate(new Vector3(0, 0, rotationAmount), spinDuration)).AppendInterval(1f).SetLoops(-1, LoopType.Incremental).Play();
    }

    // Update is called once per frame
    void Update()
    {

    }


}
