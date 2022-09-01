using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class Stats : CoreComponent
{
    //--> This code is dirty, so replace with old stats from another file.

    [SerializeField] private GameObject deathChunks;
    [SerializeField] private GameObject deathBlood;

    [SerializeField] private GameObject soulDrop;

    private GameObject mainBody;

    [SerializeField] private float maxHealth;
    [HideInInspector] public float currentHealth; // -- previously private

    [SerializeField]
    private Color damageColor = Color.red;
    private SpriteRenderer spriteRenderer;
    private Color origColor;

    protected override void Awake()
    {
        base.Awake();

        mainBody = gameObject.transform.parent.transform.parent.gameObject;
        currentHealth = maxHealth;

        spriteRenderer = gameObject.transform.parent.transform.parent.gameObject.GetComponent<SpriteRenderer>();
        origColor = spriteRenderer.color;
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

        if (gameObject != null)
        {
            Timing.RunCoroutine(ChangeColor().CancelWith(gameObject));
        }

    }

    IEnumerator<float> ChangeColor()
    {
        spriteRenderer.color = damageColor;

        yield return Timing.WaitForSeconds(0.3f);

        spriteRenderer.color = origColor;
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
}
