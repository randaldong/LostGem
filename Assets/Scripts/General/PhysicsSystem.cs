using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSystem : MonoBehaviour
{
	[Header("Collision Detection Gizmo")]
	[SerializeField] private Vector2 groundGizmoOffset;
	[SerializeField] private float groundGizmoRadius;
	[SerializeField] private Vector2 leftWallGizmoOffset;
	[SerializeField] private float leftWallGizmoRadius;
	[SerializeField] private Vector2 rightWallGizmoOffset;
	[SerializeField] private float rightWallGizmoRadius;


	[Header("Layer Masks")]
	[SerializeField] private LayerMask groundLayer;

	[System.NonSerialized] public bool isHitGround = false;
	[System.NonSerialized] public bool isLeftHitWall = false;
	[System.NonSerialized] public bool isRightHitWall = false;


	private void Update()
	{
		CheckHit();	
	}

	
	private void CheckHit()
	{
		// Hit ground
		isHitGround = Physics2D.OverlapCircle(
			(Vector2)transform.position + groundGizmoOffset, 
			groundGizmoRadius, 
			groundLayer
		);

		// Left hit wall
		isLeftHitWall = Physics2D.OverlapCircle(
			(Vector2)transform.position + leftWallGizmoOffset,
			leftWallGizmoRadius,
			groundLayer
		);

		// Right hit wall
		isRightHitWall = Physics2D.OverlapCircle(
			(Vector2)transform.position + rightWallGizmoOffset,
			rightWallGizmoRadius,
			groundLayer
		);
	}

	// Visualize collision detection gizmo
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere((Vector2)transform.position + groundGizmoOffset, groundGizmoRadius);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere((Vector2)transform.position + leftWallGizmoOffset, leftWallGizmoRadius);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere((Vector2)transform.position + rightWallGizmoOffset, rightWallGizmoRadius);

	}
}
