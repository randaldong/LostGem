using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
	private Animator animator;
	private Rigidbody2D rb;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		SetAnimation();
	}

	private void SetAnimation()
	{
		animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
	}
}
