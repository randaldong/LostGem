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

	private Rigidbody2D rb;
	private PlayerInputControl inputControl;
	private Vector2 moveDirection;
	private PhysicsManager physicsManager;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		inputControl = new PlayerInputControl();
		moveDirection = new Vector2(0, 0);
		physicsManager = GetComponent<PhysicsManager>();

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
		if (physicsManager.isOnGround)
			rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
	}
}
