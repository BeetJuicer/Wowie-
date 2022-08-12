using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{/*
    private PlayerScript player;
    private PlayerData playerData;
    private PlayerInputHandler playerInput;
   */ 
    public PlayerScript player;
    public PlayerData playerData;
    public PlayerInputHandler playerInput;

    void Awake()
    {/*
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        playerData = GameObject.FindWithTag("Player").GetComponent<PlayerData>();*/
    }

    // Update is called once per frame
    void Update()
    {

        if(player.RB.velocity.y < 0)
        {
            player.RB.velocity += Vector2.up * Physics2D.gravity * (playerData.fallMultiplier - 1) * Time.deltaTime;
        }
        else if(player.RB.velocity.y > 0 && playerInput.JumpInputStop)
        {
            player.RB.velocity += Vector2.up * Physics2D.gravity * (playerData.lowJumpMultiplier - 1) * Time.deltaTime;      
        }

        if(player.RB.velocity.y == 0 && player.StateMachine.CurrentState == player.InAirState && !playerInput.JumpInputStop)
        {
            player.RB.gravityScale = player.playerGravity * 0.5f;
           // Debug.Log("Halved gravity: " + player.RB.gravityScale);
        }
        else if(player.RB.velocity.y != 0 || player.StateMachine.CurrentState != player.InAirState || playerInput.JumpInputStop)
        {
            player.RB.gravityScale = player.playerGravity;
          //  Debug.Log("Normal gravity: " + player.RB.gravityScale);
        }

    }
}
