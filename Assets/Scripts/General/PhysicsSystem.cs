using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSystem : MonoBehaviour
{
	[Header("Collision Detection Gizmo")]
	[SerializeField] private Vector2 groundGizmoOffset = new Vector2(-0.08f, 0);
	[SerializeField] private float groundGizmoRadius = 0.2f;

	[Header("Layer Masks")]
	[SerializeField] private LayerMask groundLayer;

	[System.NonSerialized] public bool isOnGround = false;
	[System.NonSerialized] public bool isOnWall = false;

	private void Update()
	{
		CheckOnGround();	
	}

	// On the ground
	private void CheckOnGround()
	{
		isOnGround = Physics2D.OverlapCircle(
			(Vector2)transform.position + groundGizmoOffset, 
			groundGizmoRadius, 
			groundLayer
		);
	}

	// Visualize collision detection gizmo
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawWireSphere((Vector2)transform.position + groundGizmoOffset, groundGizmoRadius);
	}
}
