using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
	private Animator animator;
	private Rigidbody2D rb;
	private PhysicsSystem physics;
	private HealthStat health;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		physics = GetComponent<PhysicsSystem>();
		health = GetComponent<HealthStat>();
	}

	private void Update()
	{
		SetAnimation();
	}

	private void SetAnimation()
	{
		animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x)); // walk, run
		animator.SetFloat("velocityY", rb.velocity.y); // jump-air
		animator.SetBool("isOnGround", physics.isOnGround); // jump-land
		animator.SetBool("isDead", health.isDead); // death

	}

	public void PlayTakeDamage()
	{
		animator.SetTrigger("takeDamage");
	}

	public void PlayPlayerAttack()
	{
		animator.SetTrigger("attack");
	}

}
