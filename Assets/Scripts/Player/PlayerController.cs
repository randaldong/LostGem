using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Control Settings")]
	public float moveSpeed = 10;
	public float jumpForce = 16;

	[HideInInspector] public bool isFreeze;


	private Rigidbody2D rb;
	private PlayerInputControl inputControl;
	private Vector2 moveDirection;
	private PhysicsSystem physics;
	private StatSystem stat;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		inputControl = new PlayerInputControl();
		moveDirection = new Vector2(0, 0);
		physics = GetComponent<PhysicsSystem>();
		stat = GetComponent<StatSystem>();


		inputControl.Gameplay.Jump.started += Jump;
	}

	private void OnEnable()
	{
		inputControl.Enable();
	}

	private void OnDisable()
	{
		inputControl.Disable();
	}

	private void Update()
	{
		moveDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
	}

	// Update physics
	private void FixedUpdate()
	{
		if (!isFreeze)
			Move();
	}

	private void Move()
	{
		// x for keyboard control, y for falling
		rb.velocity = new Vector2(moveSpeed * moveDirection.x, rb.velocity.y);

		// flip when input direction is the opposite of the current direction
		float faceDir = transform.localScale.x;
		if (moveDirection.x * faceDir < 0)
			faceDir = -faceDir;
		transform.localScale = new Vector3(faceDir, transform.localScale.y, transform.localScale.z);
	}

	private void Jump(InputAction.CallbackContext context)
	{
		if (physics.isOnGround)
			rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
	}

	public void BounceAndFreeze(Transform attacker)
	{

		isFreeze = true;
		rb.velocity = Vector2.zero;
		float attackForce = attacker.GetComponent<StatSystem>().attackForce;
		Vector2 forceDir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
		rb.AddForce(forceDir * attackForce, ForceMode2D.Impulse);

	}

	public void Dead()
	{
		stat.isDead = true;
		inputControl.Gameplay.Disable();
	}
}
