using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    private List<GameObject> children = new List<GameObject>();

    private int randomNumber;
    private int lastScore;

    public float startCooldown;
    public float startSpeed;

    [HideInInspector]
    public float cannonCooldown;
    [HideInInspector]
    public float projectileSpeed;
    [HideInInspector]
    public int activeCannons = 2;

    public bool alreadyAdded;
    public bool doneActivating;

    private static CannonManager instance;

    private float elapsed = 0f;
    private void Awake()
    {
        instance = this;
    }

    public static CannonManager GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Add children to a list
        foreach (Transform child in this.gameObject.transform)
        {
            children.Add(child.gameObject);
        }

        cannonCooldown = startCooldown;
        projectileSpeed = startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeCannons > 5)
        {
            activeCannons = 5;
        }

        if(activeCannons < 1)
        {
            activeCannons = 1;
        }

        elapsed += Time.deltaTime;
        if (elapsed >= cannonCooldown && doneActivating)
        {
            doneActivating = false;
            elapsed = elapsed % cannonCooldown;

            for (int i = 0; i < activeCannons; i++)
            {
                ActivateShooter();
                doneActivating = true;
            }
        }

        if (Dice.score > 0 && !alreadyAdded)
        {
            if (Dice.score % 10 == 0)
            {
                cannonCooldown -= 0.2f;
            }

            if (Dice.score % 5 == 0)
            {
                projectileSpeed += 0.15f;
            }

            alreadyAdded = true;
            lastScore = Dice.score;
        }

        if (lastScore < Dice.score)
        {
            alreadyAdded = false;
        }

        Debug.Log("Cooldown;Speed : " + cannonCooldown + ";" + projectileSpeed);
    }

    private void ActivateShooter()
    {
        //Get a new random number.
        randomNumber = Random.Range(0, children.Count);

        children[randomNumber].GetComponent<Shooter>().warningPrefab.SetActive(true);
        children[randomNumber].GetComponent<Animator>().SetBool("fire", true);
    }

}
