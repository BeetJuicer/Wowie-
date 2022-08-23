using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private DoorBody doorBody;

    [SerializeField]
    private bool cakeTrigger;

    private Vector3 startPosition;

    private Animator animator;

    private float startTime;
    private bool clicked;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("cake", cakeTrigger);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startTime = Time.time;
            clicked = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!clicked)
            {
                if (Time.time > startTime + 0.25f)
                {
                    StepOn();
                    clicked = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && clicked)
        {
            if (clicked)
            {
                {
                    StepOff();
                }
            }
        }
    }

    void StepOn()
    {
        AudioManager.instance.Play("Click");
        doorBody.activeButtons++;
        transform.DOMoveY(startPosition.y - 0.25f, 0.2f).SetEase(Ease.InOutSine);
        if (doorBody.activeButtons > 0)
        {
            doorBody.OpenDoor();
        }
    }

    void StepOff()
    {
        doorBody.activeButtons--;
        transform.DOMoveY(startPosition.y, 0.2f).SetEase(Ease.InOutSine);
        if (doorBody.activeButtons == 0)
        {
            doorBody.CloseDoor();
        }
    }
}
