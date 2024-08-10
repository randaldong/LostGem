using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Events;

public class DamageHandler : MonoBehaviour
{
	[Header("Take Damage")]
	public UnityEvent<Transform> OnTakeDamage;
	public UnityEvent OnDead;



	private HealthStat health;
	private bool isInvincible = false;
	private Transform attacker;
	private AttackStat attack;
	private PlayerController controller;

	private void Awake()
	{
		health = GetComponent<HealthStat>();
	}


	// For damage cause by Touch Attack
	private void OnTriggerStay2D(Collider2D other)
	{
		if (!attack)
		{
			attack = other.GetComponent<AttackStat>();
			attacker = other.transform;
		}

		if (attack.attackMode == AttackStat.AttackMode.Touch)
		{
			while (!isInvincible)
			{
				StartCoroutine(TakeDamage(attack.attackDamage));
			}
		}
			
	}

	IEnumerator TakeDamage(int value)
	{
		// Enable invincible
		isInvincible = true;
		// Take damage
		if (health.curHealth <= value)
		{ // Dead
			health.curHealth = 0;
			OnDead?.Invoke();
		}
		else
		{
			health.curHealth -= value;
		}
		// Trigger: 1) take damage animation; 2) bounce & freeze
		OnTakeDamage?.Invoke(attacker);
		// Wait for invincibility time
		yield return new WaitForSeconds(health.invincibleTime);
		// Exit taking damage
		// Disable invincible
		isInvincible = false;

	}
}
