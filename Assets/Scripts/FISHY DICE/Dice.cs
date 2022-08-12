using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Dice : MonoBehaviour
{
    public bool waitingForRoll;
    public bool roll;

    public bool scoreRecorded = false;

    public float randomNumber = 0;

    public static int score;
    public static int highScore;

    public int randomAbilityNumber;

    private int diceRolls;

    private int lastCannonCount;
    private float lastProjectileSpeed;

    private int addCannon;
    private float addProjectileSpeed;
    private int addScore;

    public string ActiveAbility = " ";

    private static Dice instance;

    private PlayerHandler playerHandler;

    [SerializeField] TextMeshPro numberDiceText;
    [SerializeField] TextMeshPro abilityDiceText;
    [SerializeField] TextMeshPro additionalValuesText;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] GameObject pauseToRoll;
    [SerializeField] GameObject rollButton;

    [SerializeField] Transform position;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    public static Dice GetInstance()
    {
        return instance;
    }

    void Start()
    {
        playerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        score = 0;

        numberDiceText.maxVisibleCharacters = 5;
        lastCannonCount = CannonManager.GetInstance().activeCannons;
        lastProjectileSpeed = CannonManager.GetInstance().projectileSpeed;

    randomNumber = 1;
        randomAbilityNumber = Random.Range(1, 4);
        AssignString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.gamePaused)
        {
            //Follow the player
            gameObject.transform.position = position.position;

            scoreText.text = "Schmeckels: " + score;
            if (score > highScore)
            {
                highScore = score;
            }
            //Start a new round.
            if (randomNumber == 0 && !playerHandler.isDead)
            {
                //Record the score and high score
                if (!scoreRecorded)
                {
                    //Get a random score
                    addScore = Random.Range(0, 6);
                    score += addScore;
                    scoreRecorded = true;
                }

                //Freeze Time until dice is rolled.
                waitingForRoll = true;
                abilityDiceText.text = "";
                numberDiceText.text = "";
                pauseToRoll.SetActive(true);
                rollButton.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                DisplayValues();
            }
            
        }
    }

    public void Roll()
    {
        //Play animation
        roll = true;
        AudioManager.instance.Play("Roll");
    }

    public void NewRound()
    {
        diceRolls++;

        if (diceRolls == 2)
        {
            roll = false;

            lastCannonCount = CannonManager.GetInstance().activeCannons;
            lastProjectileSpeed = CannonManager.GetInstance().projectileSpeed;

            //Start a new roll.
            GetRandomNumbers();
            //Show Added values
            ShowAdditionalValues();

            //Disable the text
            pauseToRoll.SetActive(false);
            rollButton.SetActive(false);

            //Unfreeze Time
            Time.timeScale = 1f;
            scoreRecorded = false;
            waitingForRoll = false;

            diceRolls = 0;
        }
    }

    void GetRandomNumbers()
    {
        randomNumber = Random.Range(1, 7);
        randomAbilityNumber = Random.Range(1, 4);
        //Assign string so I don't get confused.
        AssignString();

        //Get variable numbers.
        addProjectileSpeed = Random.Range(-0.5f, 0.5f);
        addCannon = Random.Range(0, 2) * 2 - 1;
        //Add a cannon only when it's an int.
        if (CannonManager.GetInstance().activeCannons < 6)
        {
            CannonManager.GetInstance().activeCannons += (int)addCannon;
        }
        // Add projectile speed
        CannonManager.GetInstance().projectileSpeed += addProjectileSpeed;
    }

    void ShowAdditionalValues()
    {
        float f;
        f = Mathf.Round(addProjectileSpeed * 100.0f) * 0.01f;


            //Show cannons
            additionalValuesText.text = ValueSign(addScore) + addScore + " Schmeckels <br> " +
                                        ValueSign(addProjectileSpeed) + Mathf.Abs(f).ToString() + " Projectile Speed <br> " +
                                        ValueSign(addCannon) + addCannon + " Cannon";
        additionalValuesText.gameObject.SetActive(true);
        
        //Turn off after a duration.
        TurnOffAdditionalValues();
    }

    string ValueSign(float value)
    {
        if(value == 0)
        {
            return "";
        }

        string sign = "";

        if (value > 0)
        {
            sign = "+ ";
        }
        else
        {
            sign = "- ";
        }

        return sign;
    }

    IEnumerator TurnOffAdditionalValues()
    {
        yield return new WaitForSecondsRealtime(1f);
        additionalValuesText.gameObject.SetActive(false);
    }

    void AssignString()
    {
        switch (randomAbilityNumber)
        {
            case 0:
                ActiveAbility = "None";
                break;
            case 1:
                ActiveAbility = "Run";
                break;
            case 2:
                ActiveAbility = "Jump";
                break;
            case 3:
                ActiveAbility = "Dash";
                break;
            default:
                ActiveAbility = "None";
                break;
        }
    }

    void DisplayValues()
    {
        //Get 2 decimal places of randomNumber
        float f;
        f = Mathf.Round(randomNumber * 100.0f) * 0.01f;

        //Display Values to screen. Text is temporary. Find dice later.
        string unit;
        unit = (randomAbilityNumber == 1) ? "s" : "x"; // Seconds if run, Times if others
        numberDiceText.text = f.ToString() + unit;
        abilityDiceText.text = ActiveAbility;
    }
}
