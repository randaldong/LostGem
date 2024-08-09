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
	public UnityEvent OnExitTakeDamage;



	private StatSystem stat;
	private bool isInvincible = false;
	private Transform attackerTransform;
	private StatSystem attackerStat;

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
			attackerTransform = other.transform;
		}

		if (attackerStat.attackMode == StatSystem.AttackMode.Touch)
		{
			while (!isInvincible)
			{
				StartCoroutine(TakeDamage(attackerStat.damage));
			}
		}
			
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("exit trigger");
		// End take damage animation

	}
	IEnumerator TakeDamage(int value)
	{
		// Enable invincible
		isInvincible = true;
		// Take damage
		if (stat.curHealth < value) stat.curHealth = 0;
		else stat.curHealth -= value;
		// Trigger take damage animation
		OnTakeDamage?.Invoke(attackerTransform);
		// Wait for invincibility time
		yield return new WaitForSeconds(stat.invincibleTime);
		// Exit taking damage
		isInvincible = false;
		OnExitTakeDamage?.Invoke();

	}
}
