using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
	[Header("bools")]
	public bool rightHandFull;
	[Header("GO, Transforms")]
	public Animator animator;
	public Transform righthandPos, rightArm;
	public SimpleObj rightHandContaining;
	public Vector2 target;
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

	public void AimAt(Vector2 dir)
	{

		if (dir.x > 0 != isFacingRight)
		{
			Flip();
		}

		rightArm.right = isFacingRight?dir.normalized:-dir.normalized;
	}

	public void RightHandInteract()
	{
		if (rightHandFull)
		{
			rightHandContaining.InteractWith();
		}
	}

	public void LeftHandInteract()
	{

	}

	public void PickUp()
	{

	}

	public void PickUp(SimpleObj obj)
	{

	}
	#endregion

	#region PrivateFunctions
	protected override void Start()
	{
		base.Start();
		if (rightHandFull)
		{
			rightHandContaining.GetPickedUpBy(this);
		}
	}
	#endregion
}
