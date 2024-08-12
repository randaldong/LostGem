using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
	public int hitWallFreezeTime;

	protected float curSpeed;
	Vector3 faceDir;

    Rigidbody2D rb;
    protected Animator anim;
	PhysicsSystem physics;
	bool isWait;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		physics = GetComponent<PhysicsSystem>();
		curSpeed = walkSpeed;
	}

	private void Update()
	{
		faceDir = new Vector3(-transform.localScale.x, 0, 0);

		// Turn around if hit wall
		if (!isWait && 
			(physics.isLeftHitWall && faceDir.x < 0 || 
			physics.isRightHitWall && faceDir.x > 0))
		{
			isWait = true;
			curSpeed = 0;
			anim.SetBool("isWalk", false);
			StartCoroutine(WaitToTurn());
		}

	}
	private void FixedUpdate()
	{
		Move();
	}

	public virtual void Move()
	{
		rb.velocity = new Vector2(curSpeed * faceDir.x, rb.velocity.y);
	}

	IEnumerator WaitToTurn() { 
		yield return new WaitForSeconds(hitWallFreezeTime);
		transform.localScale = new Vector3(faceDir.x, transform.localScale.y, transform.localScale.z);
		anim.SetBool("isWalk", true);
		curSpeed = walkSpeed;
		isWait = false;
	}
}
