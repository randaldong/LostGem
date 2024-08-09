using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Events;

public class CombatSystem : MonoBehaviour
{
	[Header("Take Damage")]
	public UnityEvent<Transform> OnTakeDamage;
	public UnityEvent OnDead;



	private StatSystem stat;
	private bool isInvincible = false;
	private Transform attacker;
	private StatSystem attackerStat;
	private PlayerController controller;

	private void Awake()
	{
		stat = GetComponent<StatSystem>();
	}


	// For damage cause by Touch Attack
	private void OnTriggerStay2D(Collider2D other)
	{
		if (!attackerStat)
		{
			attackerStat = other.GetComponent<StatSystem>();
			attacker = other.transform;
		}

		if (attackerStat.attackMode == StatSystem.AttackMode.Touch)
		{
			while (!isInvincible)
			{
				StartCoroutine(TakeDamage(attackerStat.attackDamage));
			}
		}
			
	}

	IEnumerator TakeDamage(int value)
	{
		// Enable invincible
		isInvincible = true;
		// Take damage
		if (stat.curHealth <= value)
		{ // Dead
			stat.curHealth = 0;
			OnDead?.Invoke();
		}
		else
		{
			stat.curHealth -= value;
		}
		// Trigger: 1) take damage animation; 2) bounce & freeze
		OnTakeDamage?.Invoke(attacker);
		// Wait for invincibility time
		yield return new WaitForSeconds(stat.invincibleTime);
		// Exit taking damage
		// Disable invincible
		isInvincible = false;

	}
}
