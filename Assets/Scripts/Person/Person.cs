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
	protected int collidings = 0;
	//[Header("bools")]
	protected bool isFacingRight = true;
	protected bool isRunning, isJumping, isSliding;
	protected bool grounded;
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions

	public void Jump()
	{
		if(grounded)
			rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
	}

	public void Move(int dir)
	{

		rb.velocity = dir * Vector2.right * moveSpeed + new Vector2(0, rb.velocity.y);
	}

	public void StartMovingForward()
	{
		if (isFacingRight)
		{
			animator.SetBool("WalkingForward", true);
		}
		else
		{
			animator.SetBool("WalkingBackwards", true);
		}
	}

	public void StartMovingBackwards()
	{
		if (isFacingRight)
		{
			animator.SetBool("WalkingBackwards", true);
		}
		else
		{
			animator.SetBool("WalkingForward", true);
		}
	}

	public void StopMovingForward()
	{
		if (isFacingRight)
		{
			animator.SetBool("WalkingForward", false);
		}
		else
		{
			animator.SetBool("WalkingBackwards", false);
		}
	}

	public void StopMovingBackwards()
	{
		if (isFacingRight)
		{
			animator.SetBool("WalkingBackwards", false);
		}
		else
		{
			animator.SetBool("WalkingForward", false);
		}
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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		collidings++;
		grounded = collidings > 0;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		collidings--;
		grounded = collidings > 0;
	}
	#endregion
}
