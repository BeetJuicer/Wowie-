using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCounter : MonoBehaviour
{
    bool cakePassed;
    bool iceCreamPassed;

    private Animator animator;
    [SerializeField] private int pointNumber;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (pointNumber <= GameManager.GetInstance().checkPointCount)
        {
            animator.Play("Checkpoint-2");
        }
        else
        {
            animator.Play("Checkpoint-0");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pointNumber > GameManager.GetInstance().checkPointCount)
        {
            if (cakePassed && iceCreamPassed)
            {
                GameManager.GetInstance().checkPointCount++;
                cakePassed = false;
                iceCreamPassed = false;
                animator.Play("Checkpoint-2");
                AudioManager.instance.Play("coin");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pointNumber > GameManager.GetInstance().checkPointCount)
        {
            if (collision.CompareTag("Player"))
            {
                if (collision.GetComponent<PlayerHandler>().cake && !cakePassed)
                {
                    cakePassed = true;
                    animator.Play("Checkpoint-1-cake");
                    AudioManager.instance.Play("coin");
                }
                if (!collision.GetComponent<PlayerHandler>().cake && !iceCreamPassed)
                {
                    iceCreamPassed = true;
                    animator.Play("Checkpoint-1-iceCream");
                    AudioManager.instance.Play("coin");
                }
            }
        }
    }
}
