using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    [SerializeField] private GameObject deathChunks;
    [SerializeField] private GameObject deathBlood;
    private GameObject mainBody;

    [SerializeField] private float maxHealth;
    private float currentHealth;

    protected override void Awake()
    {
        base.Awake();
        mainBody = gameObject.transform.parent.transform.parent.gameObject;
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Instantiate(deathChunks, transform.position, Quaternion.identity);
            Instantiate(deathBlood, transform.position, Quaternion.identity);
            if (mainBody.name != "Player")
            {
                Destroy(mainBody);
            }
            else
            {
                //put code here for respawning back to camp
            }
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
