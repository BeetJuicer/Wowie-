using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    //--> This code is dirty, so replace with old stats from another file.

    [SerializeField] private GameObject deathChunks;
    [SerializeField] private GameObject deathBlood;

    [SerializeField] private GameObject soulDrop;
    private GameObject mainBody;

    [SerializeField] private float maxHealth;
    //private float currentHealth;
    [HideInInspector] public float currentHealth;

    protected override void Awake()
    {
        base.Awake();
        mainBody = gameObject.transform.parent.transform.parent.gameObject;
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            if (mainBody.CompareTag("Player"))
            {
                GameManager.GetInstance().Respawn();
                mainBody.SetActive(false);
            }

            if (!mainBody.CompareTag("Player"))
            {
                if (mainBody.CompareTag("Enemy"))
                {
                    Instantiate(soulDrop, mainBody.transform.position, Quaternion.identity);
                }
                mainBody.SetActive(false);
            }

            Instantiate(deathChunks, transform.position, Quaternion.identity);
            Instantiate(deathBlood, transform.position, Quaternion.identity);
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
    
    public void IncreaseMaxHealth(float multiplier)
    {
        maxHealth += maxHealth * multiplier;
        currentHealth += maxHealth * multiplier;
    }

    private void Update()
    {
        if (mainBody.CompareTag("Player"))
        {
            DebugPanel.Log("Current Health", currentHealth);
            DebugPanel.Log("Max Health", maxHealth);
        }
    }
}
