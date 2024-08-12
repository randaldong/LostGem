using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Events;
using System.Drawing;
using Unity.VisualScripting;

public class HealthManager : MonoBehaviour
{
	[SerializeField] private AttackerType attackerType=AttackerType.Untagged;
	public UnityEvent<Transform> OnTakeDamage;
	public UnityEvent OnDead;

	private HealthStat health;
	private bool isInvincible = false;
	private Transform attacker;
	private AttackStat attack;

	private enum AttackerType
	{
		Untagged,
		Player,
		Mob,
	};

	private void Awake()
	{
		health = GetComponent<HealthStat>();
	}


	// For damage cause by Touch Attack
	private void OnTriggerStay2D(Collider2D other)
	{
		if (!other.CompareTag(attackerType.ToString())) return;

		attack = other.GetComponent<AttackStat>();
		if (attack.attackMode != AttackStat.AttackMode.Touch) return;

		attacker = other.transform;

		if (!isInvincible) // continous damage for touch attack
		{
			StartCoroutine(TakeTouchDamage(attack.attackDamage));
		}
	}

	// For damage cause by Hit Attack
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag(attackerType.ToString())) return;

		attack = other.GetComponent<AttackStat>();
		if (attack.attackMode != AttackStat.AttackMode.Hit) return;

		attacker = other.transform;

		// one-time damage for hit attack
		TakeHitDamage(attack.attackDamage);
		
	}

	IEnumerator TakeTouchDamage(int value)
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
		// Disable invincible
		isInvincible = false;

	}

	private void TakeHitDamage(int value)
	{
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
	}
}
