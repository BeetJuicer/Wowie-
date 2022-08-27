using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedBuff : MonoBehaviour
{
    private float attackSpeedMultiplier;
    private SoulStats soulStats;

    private bool playerDetected;
    // Start is called before the first frame update
    void Start()
    {
        attackSpeedMultiplier = Random.Range(0.01f, 0.1f); //---> 1% to 10%
        soulStats = GameObject.FindGameObjectWithTag("Player").GetComponent<SoulStats>();
    }

    private void Update()
    {
        if (playerDetected)
        {
            //Display an interact button / UI element with a description at the player's position?
            if (PlayerInputHandler.GetInstance().GetInteractPressed())
            {
                soulStats.IncreaseAttackSpeed(attackSpeedMultiplier);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Disable the interact button
        playerDetected = false;
    }
}
