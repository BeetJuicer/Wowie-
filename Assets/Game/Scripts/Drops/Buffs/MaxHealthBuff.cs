using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthBuff : MonoBehaviour
{
    private float maxHealthMultiplier;
    private SoulStats soulStats;

    private bool playerDetected;
    // Start is called before the first frame update
    void Start()
    {
        maxHealthMultiplier = Random.Range(0.05f, 0.3f); //---> 5% to 30%
        soulStats = GameObject.FindGameObjectWithTag("Player").GetComponent<SoulStats>();
    }

    private void Update()
    {
        if (playerDetected)
        {
            //Display an interact button / UI element with a description at the player's position?
            if (PlayerInputHandler.GetInstance().GetInteractPressed())
            {
                soulStats.IncreaseMaxHealth(maxHealthMultiplier);
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
