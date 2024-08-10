using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStat : MonoBehaviour
{
	public float maxHealth;
	public float curHealth;
	public float invincibleTime;
	public bool isDead;

	private void Start()
	{
		curHealth = maxHealth;
	}
}
