using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

	float curSpeed;
	Vector3 faceDir;

    Rigidbody2D rb;
    Animator anim;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		curSpeed = walkSpeed;
	}

	private void Update()
	{
		faceDir = new Vector3(-transform.localScale.x, 0, 0);
	}
	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		rb.velocity = new Vector2(curSpeed * faceDir.x, rb.velocity.y);
	}
}
