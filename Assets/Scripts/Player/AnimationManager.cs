using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
	private Animator animator;
	private Rigidbody2D rb;
	private PhysicsManager physicsManager;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		physicsManager = GetComponent<PhysicsManager>();
	}

	private void Update()
	{
		SetAnimation();
	}

	private void SetAnimation()
	{
		animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
		animator.SetFloat("velocityY", rb.velocity.y);
		animator.SetBool("isOnGround", physicsManager.isOnGround);
	}
}
