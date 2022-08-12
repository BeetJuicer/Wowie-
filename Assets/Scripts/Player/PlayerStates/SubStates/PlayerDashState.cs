using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }
    private bool isHolding;
    private bool dashInputStop;
    
    private float lastDashTime;

    private Vector2 firstPos;
    private Vector2 lastPos;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;

    public PlayerDashState(PlayerScript player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        firstPos = player.transform.position;

        CanDash = false;
        player.InputHandler.UseDashInput();

        isHolding = true;
        dashDirection = Vector2.right * player.FacingDirection;
        //Dash slow motion is slower in tutorial.
        if (playerData.isTutorial)
        {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }
        else
        {
            Time.timeScale = playerData.holdTimeScale; // 1/0.25 = 4 times slower
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }

        startTime = Time.unscaledTime;

        player.DashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.trails.gameObject.SetActive(false);

        if(player.CurrentVelocity.y > 0)
        {
            player.SetVelocityY(player.CurrentVelocity.y * playerData.dashEndYMultiplier);
        }
        if(player.FacingDirection == -1)
        {
            player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if(player.FacingDirection == 1)
        {
            player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        player.abilityUsed = "dash";

        //Count the dash if ability is dash
        if (Dice.GetInstance().ActiveAbility == "Dash")
        {
            Dice.GetInstance().randomNumber--;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!player.CheckIfGrounded())
        {
            player.Anim.SetBool("inAir", true);
        }
        else
        {
            player.Anim.SetBool("inAir", false);
        }

        if (!isExitingState )
        {
            //player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            //player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));

            if (isHolding)
            {
                dashDirectionInput = player.InputHandler.DashDirectionInput;
                dashInputStop = player.InputHandler.DashInputStop;
                //Debug.Log(player.InputHandler.DashInputStop);

                if(dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                player.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
                player.DashDirectionIndicator.rotation = Quaternion.Euler(0, 0, angle - 45f);//subtract 45 because sprite is originally at an angle.
                if (playerData.isTutorial)
                {
                    if (dashInputStop)
                    {
                        // Debug.Log("dashInputStop is: " + dashInputStop + " Time over is: " + (Time.unscaledTime >= startTime + playerData.maxHoldTime));
                        player.trails.gameObject.SetActive(true);
                        isHolding = false;
                        Time.timeScale = 1;
                        Time.fixedDeltaTime = playerData.defaultFixedDeltaTime;
                        startTime = Time.time;
                        player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                        player.RB.drag = playerData.drag;
                        player.SetVelocity(playerData.dashVelocity, dashDirection);
                        player.DashDirectionIndicator.gameObject.SetActive(false);
                    }
                }
                else if(dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime)
                {
                    // Debug.Log("dashInputStop is: " + dashInputStop + " Time over is: " + (Time.unscaledTime >= startTime + playerData.maxHoldTime));
                    player.trails.gameObject.SetActive(true);

                    isHolding = false;
                    Time.timeScale = 1;
                    Time.fixedDeltaTime = playerData.defaultFixedDeltaTime;
                    startTime = Time.time;

                    AudioManager.instance.Play("Dash");
                    AudioManager.instance.Play("DashOut");
                    player.SpawnDashFX(angle + 90f);
                    //player.shakerDash.FireOnce();

                    player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.RB.drag = playerData.drag;
                    player.SetVelocity(playerData.dashVelocity, dashDirection);
                    player.DashDirectionIndicator.gameObject.SetActive(false);
                }
            }
            else
            {
                if (playerData.isTutorial)
                {
                    player.SetVelocity(playerData.dashVelocity + 3f, dashDirection);
                }
                else
                {
                    player.SetVelocity(playerData.dashVelocity, dashDirection);
                }
                
                if(Time.time >= startTime + playerData.dashTime)
                {
                    player.RB.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }

    public bool CheckIfCanDash()
    {
        return false;// CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }

    public void ResetCanDash()
    {
        CanDash = true;
    }

}
