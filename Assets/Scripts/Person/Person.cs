using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Person : SimpleObj
{
	[Header("Person vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	[Header("floats")]
	public float moveSpeed = 10, jumpSpeed = 15;
	//[Header("ints")]
	//[Header("bools")]
	[Header("GO, Transforms")]
	public Animator animator;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	protected bool isFacingRight = true;
	protected bool isRunning, isJumping, isSliding;
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions

	public void Jump()
	{
		rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
	}

	public void Move(int dir)
	{
		rb.velocity = dir * Vector2.right * moveSpeed + new Vector2(0, rb.velocity.y);
	}

	public void StartMovingForward()
	{
		animator.SetBool("WalkingForward", true);
	}

	public void StartMovingBackwards()
	{
		animator.SetBool("WalkingBackwards", true);
	}

	public void StopMovingForward()
	{
		animator.SetBool("WalkingForward", false);
	}

	public void StopMovingBackwards()
	{
		animator.SetBool("WalkingBackwards", false);
	}

	public void Flip()
	{
		isFacingRight = !isFacingRight;
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	#endregion

	#region PrivateFunctions
	protected override void Start()
	{
		base.Start();
	}
	#endregion
}
