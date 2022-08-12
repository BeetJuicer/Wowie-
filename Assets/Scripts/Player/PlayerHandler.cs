using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerHandler : MonoBehaviour
{
    private float startTime;
    private float originalIntensity;

    private bool animationFinished;
    private bool isGameOverMenuActive;
    private bool doneDying;

    public bool cake;
    public bool isDead;

    private Animator anim;
    private Player player;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject playerLight;
    [SerializeField] private GameObject dangerLight;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject deathChunkParticle;
    [SerializeField] private GameObject deathBloodParticle;

    void Start()
    {
        AudioManager.instance.Play("Theme_1");

        anim = gameObject.GetComponent<Animator>();
        player = gameObject.GetComponent<Player>();
        animationFinished = false;
        isGameOverMenuActive = false;
        isDead = false;
        anim.SetBool("dead", false);
        scoreText.SetActive(false);  
    }



    void Update()
    {

        // Don't do anything if game is paused.
        if (DialogueManager.GetInstance().dialogueIsPlaying || 
            CutsceneTracker.GetInstance().cutsceneIsPlaying ||
            PauseMenu.gamePaused)
        {
            return;
        }

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

            //--Kill player.
            DamageFeedback();
        //    player.SetVelocityZero();
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

    private IEnumerator StopPickUp()
    {
        yield return new WaitForSeconds(.2f);
        playerLight.GetComponent<Animator>().SetBool("pickUp", false);
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
}
