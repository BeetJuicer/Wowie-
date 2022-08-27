using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulStats : MonoBehaviour
{
    [SerializeField]
    private Animator weaponAnim;

    [HideInInspector]
    public int soulCount = 0;
    public float attackSpeedMultiplier = 1;

    private void Update()
    {
        weaponAnim.SetFloat("attackSpeed", attackSpeedMultiplier);
    }
}
