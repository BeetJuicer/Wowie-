﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable {

	private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
	private CollisionSenses CollisionSenses {
		get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
	}
	private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }

	private Movement movement;
	private CollisionSenses collisionSenses;
	private Stats stats;

	[SerializeField] private float maxKnockbackTime = 0.2f;

	private bool isInvincible;

	private bool isKnockbackActive;
	private float knockbackStartTime;

	public override void LogicUpdate() {
		CheckKnockback();
	}

	public void Damage(float amount) {
		if (!isInvincible)
        {
		//	Debug.Log(core.transform.parent.name + " was damaged for " + amount + " damage!");
			Stats?.DecreaseHealth(amount);
        }
	}

	public void Knockback(Vector2 angle, float strength, int direction) 
	{
		if (!isInvincible)
        {
			Movement?.SetVelocity(strength, angle, direction);
			Movement.CanSetVelocity = false;
			isKnockbackActive = true;
			knockbackStartTime = Time.time;
        }
	}

	private void CheckKnockback() {
		if (isKnockbackActive
			&& ((Movement?.CurrentVelocity.y <= 0.01f && CollisionSenses.Ground)
					|| Time.time >= knockbackStartTime + maxKnockbackTime)
			) 
		{
			isKnockbackActive = false;
			Movement.CanSetVelocity = true;
		}
	}

	public void SetInvincible(bool invincibility)
    {
		isInvincible = invincibility;
    }
}
