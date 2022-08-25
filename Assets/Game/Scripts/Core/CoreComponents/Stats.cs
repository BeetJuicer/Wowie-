using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject deathChunks;
    [SerializeField] private GameObject deathBlood;
    private float currentHealth;

    protected override void Awake()
    {
        base.Awake();

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
            Destroy(gameObject.transform.parent.transform.parent.gameObject);
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
