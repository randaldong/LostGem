using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Mob
{
	public override void Move()
	{
		base.Move();
		anim.SetBool("isWalk", true);
	}
}
