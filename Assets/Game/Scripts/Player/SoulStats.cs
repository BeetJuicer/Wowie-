using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulStats : MonoBehaviour
{
    [SerializeField]
    private Animator weaponAnim;

    private Stats stats;

    [HideInInspector]
    public int soulCount = 0;
    public float attackSpeed = 1;
    public float attackDamageMultiplier = 0;
    public float healthMultiplier = 0;

    private void Start()
    {
        stats = GetComponentInChildren<Stats>();
    }

    private void Update()
    {
        weaponAnim.SetFloat("attackSpeed", attackSpeed);
    }

    public void IncreaseAttackSpeed(float multiplier)
    {
        float speedToAdd = attackSpeed * multiplier;
        attackSpeed += speedToAdd;
    }
    
    public void IncreaseMaxHealth(float multiplier)
    {
        healthMultiplier += multiplier;
        stats.IncreaseMaxHealth(healthMultiplier);

    }
    
    public void IncreaseAttackDamage(float multiplier)
    {
        attackDamageMultiplier += multiplier;
    }
}
