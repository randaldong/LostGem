using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStat : MonoBehaviour
{
	public int attackDamage;
	public float attackForce;
	public float attackRange;
	public float attackSpeed;
	public AttackMode attackMode;


	// Hide from inspector
	[HideInInspector]
	public enum AttackMode
	{
		Touch, // cause damage if touched
		Hit, // cause damage if attack & hit
	}
}
