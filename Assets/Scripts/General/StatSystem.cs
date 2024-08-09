using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSystem : MonoBehaviour
{
	// Inspector
    [Header("Health")]
    public float maxHealth;
    public float curHealth;
	public float invincibleTime;

	[Header("Attack")]
	public int damage;
	public float range;
	public float speed;
	public AttackMode attackMode;

	

	// Hide from inspector
	[HideInInspector]public enum AttackMode
	{
		Touch, // cause damage if touched
		Hit, // cause damage if attack & hit
	}
	private void Start()
	{
		curHealth = maxHealth;
	}

	
}
