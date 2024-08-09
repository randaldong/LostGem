using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
	private Animator animator;
	private Rigidbody2D rb;
	private PhysicsSystem physics;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		physics = GetComponent<PhysicsSystem>();
	}

	private void Update()
	{
		SetAnimation();
	}

	private void SetAnimation()
	{
		animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
		animator.SetFloat("velocityY", rb.velocity.y);
		animator.SetBool("isOnGround", physics.isOnGround);
	}

	public void PlayTakeDamage()
	{
		animator.SetTrigger("takeDamage");
	}

	public void ExitTakeDamage()
	{
		animator.SetTrigger("exitTakeDamage");
	}

}
