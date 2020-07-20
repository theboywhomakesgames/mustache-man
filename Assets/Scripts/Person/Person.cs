using Assets.Scripts.Object;
using System;
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
	public float health = 100;
	public float moveSpeed = 10, jumpSpeed = 15;
	public float deathKick = 50;
	//[Header("ints")]
	[Header("bools")]
	public bool rightHandFull;
	public bool isAlive = true;
	[NonSerialized]
	public bool movingForward, movingBackward;
	[Header("GO, Transforms")]
	public Animator animator;
	public Transform righthandPos, rightArm;
	public InteractableObj rightHandContaining;
	public Vector2 target;
	public GameObject bloodDropPref, bloodPSPref;
	public EventHolder onDeath, onDamage;

	public List<InteractiveObj> nearbyInteractives = new List<InteractiveObj>();
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
	protected Vector2 lastAssualtPos;
	protected Vector2 lastAssualtDir;
	#endregion

	#region PublicFunctions

	public void Jump()
	{
		if(isAlive)
		{
			if (grounded)
				rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
		}
	}

	public void Move(int dir)
	{
		if (isAlive)
		{
			rb.velocity = dir * Vector2.right * moveSpeed + new Vector2(0, rb.velocity.y);
		}
	}

	public void StartMovingForward()
	{
		if (isAlive)
		{
			movingForward = true;
			if (isFacingRight)
			{
				animator.SetBool("WalkingForward", true);
			}
			else
			{
				animator.SetBool("WalkingBackwards", true);
			}
		}
	}

	public void StartMovingBackward()
	{
		if (isAlive)
		{
			movingBackward = true;
			if (isFacingRight)
			{
				animator.SetBool("WalkingBackwards", true);
			}
			else
			{
				animator.SetBool("WalkingForward", true);
			}
		}
	}

	public void StopMovingForward()
	{
		if (isAlive)
		{
			if (isFacingRight)
			{
				animator.SetBool("WalkingForward", false);
			}
			else
			{
				animator.SetBool("WalkingBackwards", false);
			}
			movingForward = false;
		}
	}

	public void StopMovingBackward()
	{
		if (isAlive)
		{
			if (isFacingRight)
			{
				animator.SetBool("WalkingBackwards", false);
			}
			else
			{
				animator.SetBool("WalkingForward", false);
			}
			movingBackward = false;
		}
	}

	public void Flip()
	{
		isFacingRight = !isFacingRight;
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

		if (movingForward)
		{
			StopMovingForward();
			StartMovingBackward();
		}
		else if (movingBackward)
		{
			StopMovingBackward();
			StartMovingForward();
		}
	}

	public void AimAt(Vector2 dir)
	{
		if (isAlive)
		{
			if (dir.x > 0 != isFacingRight)
			{
				Flip();
			}

			rightArm.right = isFacingRight ? dir.normalized : -dir.normalized;
		}
	}

	public void RightHandInteract()
	{
		if (isAlive)
		{
			if (rightHandFull)
			{
				rightHandContaining.InteractWith();
			}
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

	public void InteractWithNearby()
	{
		try
		{
			nearbyInteractives[0].Interact(this);
		}
		catch { }
	}

	public virtual void Damage(float volume, float x = 0, float y = 0, float bx = 0, float by = 0)
	{
		health -= volume;
		lastAssualtPos = new Vector2(x, y);
		lastAssualtDir = new Vector2(bx, by).normalized;

		try
		{
			onDamage.Excecute();
		}
		catch { }

		CheckHealth();
	}
	#endregion

	#region PrivateFunctions
	protected virtual void CheckHealth()
	{
		if(health <= 0)
		{
			health = 0;
			Die();
		}
	}

	protected virtual void Die()
	{
		if (isAlive)
		{
			isAlive = false;
			if (rightHandFull)
			{
				rightHandContaining.GetDropped(new Vector2(UnityEngine.Random.Range(-1, 1f), UnityEngine.Random.Range(-1, 1f)).normalized);
			}
			rb.constraints = RigidbodyConstraints2D.None;
			Instantiate(bloodDropPref, lastAssualtPos, Quaternion.identity).GetComponent<BloodDrop>().Throw(lastAssualtDir);
			Instantiate(bloodPSPref, lastAssualtPos, Quaternion.identity);
			Vector2 dir = ((Vector2)transform.position - lastAssualtPos).normalized;
			rb.AddForceAtPosition(lastAssualtDir * 1000 * deathKick * 0.01f / Time.fixedDeltaTime, lastAssualtPos);
			try
			{
				onDeath.Excecute();
			}
			catch { }
		}
	}

	protected virtual void ComeAlive()
	{
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		isAlive = true;
	}

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
