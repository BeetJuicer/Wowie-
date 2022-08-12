using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerHandler : MonoBehaviour
{
    //public float maxTime = 15;//change back to 20
    //[SerializeField]private float timeLeft;
    private float startTime;
    private float originalIntensity;

    private bool animationFinished;
    private bool isGameOverMenuActive;
    private bool doneDying;
    //public bool isTimeActive;
    public bool cake;
    public bool isDead;

    private GameManager GM;
    private Animator anim;
    //private HealthBar timeBar;
    private PlayerScript player;
    [SerializeField] private PlayerData playerData;
    //[SerializeField] private ShakeIn2D camShaker;
    [SerializeField] private GameObject playerLight;
    [SerializeField] private GameObject dangerLight;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject scoreText;
    //[SerializeField] private Animator timeBarAnim;
    //private AudioManager audioManager;

    [SerializeField] private GameObject deathChunkParticle;
    [SerializeField] private GameObject deathBloodParticle;

    void Start()
    {
        //--TODO: Change time to HP.

        AudioManager.instance.Play("Theme_1");

        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
       // timeBar = GameObject.Find("Time Bar").GetComponent<HealthBar>();
        anim = gameObject.GetComponent<Animator>();
        player = gameObject.GetComponent<PlayerScript>();
        animationFinished = false;
        isGameOverMenuActive = false;
        isDead = false;
        anim.SetBool("dead", false);
        scoreText.SetActive(false);
        //--Set damageable 0.75 seconds after starting.
        //Invoke("ActivateTime", 0.75f);

        playerData.defaultFixedDeltaTime = Time.fixedDeltaTime;
        //-- Set HP for player.
    //    timeLeft = maxTime;
    //    timeBar.SetMaxTime(maxTime);
        
    }

    //--Activate Damageable
    private void ActivateTime()
    {
       // isTimeActive = true;
    }

    void Update()
    {

        if ((cake == true) && (GameManager.isCake == false))
        {
            return;
        }

        // Don't do anything if game is paused.
        if (DialogueManager.GetInstance().dialogueIsPlaying || 
            CutsceneTracker.GetInstance().cutsceneIsPlaying ||
            PauseMenu.gamePaused)
        {
            return;
        }

        /*-- If damageable, damage
        if (isTimeActive)
        {
            timeLeft -= Time.deltaTime;
        }

        //-- Activate danger light if close to dying. Change to ternary operator to clean.
        if (timeLeft < (maxTime * 0.15f))
        {
            dangerLight.SetActive(true);
        }
        else
        {
            dangerLight.SetActive(false);
        }*/

        // Blink the player light according to stamina left. Change to ternary.
        /*
        if (playerData.currentStamina < playerData.maxStamina * 0.15f)
        {
            playerLight.GetComponent<Animator>().SetBool("blink", true);
        }
        else
        {
            playerLight.GetComponent<Animator>().SetBool("blink", false);
        }*/

        //--Update UI responsible for showing health.
       // timeBar.SetTime(timeLeft);
       
        /*
        if (some condition, maybe health)
        {
            isDead = true;
        }*/

        if (isDead)
        {   
            //--Disable all animations on death -- remove this when death state has been made.
            anim.SetBool("idle", false);
            anim.SetBool("inAir", false);
            anim.SetBool("move", false);
            anim.SetBool("land", false);
            anim.SetBool("wallGrab", false);
            anim.SetBool("wallSlide", false);
            anim.SetBool("wallClimb", false);
            anim.SetBool("ledgeClimbState", false);

            //--Set back time to normal in case dash was active.
            Time.timeScale = 1;
            Time.fixedDeltaTime = playerData.defaultFixedDeltaTime;

            //--Kill player.
            DamageFeedback();
            player.SetVelocityZero();
            if (!doneDying)
            {
                anim.SetBool("death", true);
                doneDying = true;
            }
            
            if (animationFinished)
            {
                //--Respawn player after playing death animation.
                anim.SetBool("death", false);
                anim.SetBool("dead", true);
                AudioManager.instance.Play("Hurt");
                AudioManager.instance.Play("GameOver");

                StartCoroutine(Death());
                animationFinished = false;
            }
        }

        //--If health is above a certain threshold, return to neutral light.
        if (Time.time > startTime + .3f)
        {
            TurnNormal();
        }
    }

    IEnumerator Death()
    {  
       
        yield return new WaitForSeconds(1.5f);

        if (!isGameOverMenuActive)
        {
            scoreText.SetActive(false);
            AudioManager.instance.Stop("Theme_1");
            AudioManager.instance.Play("Theme_2");

            gameOverMenu.SetActive(true);
            isGameOverMenuActive = true;
        }
    }

    /* Change this to RecoverHealth
    public void AddTime(int addedTime)
    {
        if (addedTime > 0)
        {
        playerLight.GetComponent<Animator>().SetBool("pickUp", true);
        }

        if ((timeLeft + addedTime) > maxTime)
        {
            timeLeft = maxTime;
        }
        else if ((timeLeft + addedTime) <= maxTime)
        {
            timeLeft += addedTime;
        }
        StartCoroutine(StopPickUp());
    }
    */
    private IEnumerator StopPickUp()
    {
        yield return new WaitForSeconds(.2f);
        playerLight.GetComponent<Animator>().SetBool("pickUp", false);
    }
    
    private IEnumerator StopKnockBack()
    {
        yield return new WaitForSeconds(.05f);
        player.SetVelocityZero();
    }

    private void SpawnChunks()
    {
        Instantiate(deathBloodParticle, gameObject.transform.position, deathBloodParticle.transform.rotation);
        Instantiate(deathChunkParticle, gameObject.transform.position, deathChunkParticle.transform.rotation);
    }
    private void FinishAnimation()
    {
        animationFinished = true;
    }

    public void DamageFeedback()
    {
        startTime = Time.time;
        //--Apply screenshake
        //camShaker.FireOnce(); -- Change this to dotween screenshake
        //--Update UI health indicator
        //timeBarAnim.SetBool("hurt", true); -- UI indicator for health, still no object
        //--Apply knockback.
        //Knockback();
        //--Apply feedback to light
        playerLight.GetComponent<Animator>().SetBool("damaged", true);
        //--Set player color to gray
        player.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void TurnNormal()
    {
        player.GetComponent<SpriteRenderer>().color = Color.white;
        //timeBarAnim.SetBool("hurt", false); -- UI indicator for health
        playerLight.GetComponent<Animator>().SetBool("damaged", false);
    }

    public void Knockback()
    {
        player.RB.AddForce(new Vector2(500f * player.FacingDirection, 500f * Mathf.Sign(player.CurrentVelocity.y)));//adjust according to direction x * Facingdirection y * yVelocity
        StartCoroutine(StopKnockBack());
    }
}
