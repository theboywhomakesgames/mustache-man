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
	public void Move(int dir)
	{
		rb.velocity = dir * Vector2.right * moveSpeed + new Vector2(0, rb.velocity.y);
	}

	public void StartMoving()
	{
		animator.SetBool("Walking", true);
	}

	public void StopMoving()
	{
		animator.SetBool("Walking", false);
	}
	#endregion

	#region PrivateFunctions
	protected override void Start()
	{
		base.Start();
	}
	#endregion
}
