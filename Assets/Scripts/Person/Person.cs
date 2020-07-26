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
	public float moveSpeed = 10, jumpSpeed = 15, gravityMagnitude = 20;
	public float deathKick = 50;
	//[Header("ints")]
	[Header("bools")]
	public bool rightHandFull;
	public bool isAlive = true;
	public bool movingRight, movingLeft;
	[Header("GO, Transforms")]
	public Animator animator;
	public Transform righthandPos, rightArm;
	public InteractableObj rightHandContaining;
	public Vector2 target;
	public GameObject bloodDropPref, bloodPSPref;
	public EventHolder onDeath, onDamage;

	public List<InteractiveObj> nearbyInteractives = new List<InteractiveObj>();

	public Vector2 gravity;

	public AudioClip walkclip, jumpclip;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	protected int collidings = 0;
	//[Header("bools")]
	protected bool isFacingRight = true;
	protected bool isRunning, isJumping, isSliding;
	protected bool grounded, flipped;
	//[Header("GO, Transforms")]
	protected Vector2 lastAssualtPos;
	protected Vector2 lastAssualtDir;

	protected Vector2 lastVel;
	protected Vector2 newVel;
	protected Vector2 acceleration;
	protected Vector2 myRight = Vector2.right;

	protected bool hasPlayer = false;
	protected AudioPlayer ap;
	#endregion

	#region PublicFunctions

	public void PlayJumpSFX()
	{
		if (!hasPlayer)
			GetAudioPlayer();

		ap.as_.PlayOneShot(jumpclip);
	}

	public void PlayWalkSFX()
	{
		if (!hasPlayer)
			GetAudioPlayer();

		ap.as_.PlayOneShot(walkclip);
	}

	public void Jump()
	{
		if(isAlive && grounded)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.gravityScale * jumpSpeed);
			PlayJumpSFX();
		}
	}

	public void Move(int dir)
	{
		if (isAlive)
		{
			rb.velocity = dir * myRight * moveSpeed + new Vector2(0, rb.velocity.y);
		}
	}

	public void StartMovingRight()
	{
		if (isAlive)
		{
			movingRight = true;
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

	public void StartMovingLeft()
	{
		if (isAlive)
		{
			movingLeft = true;
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

	public void StopMovingRight()
	{
		if (isAlive)
		{
			if (!flipped)
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
			else
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
			flipped = false;
			movingRight = false;
		}
	}

	public void StopMovingLeft()
	{
		if (isAlive)
		{
			if (!flipped)
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
			else
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
			flipped = false;
			movingLeft = false;
		}
	}

	public void Flip()
	{
		isFacingRight = !isFacingRight;
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

		if(movingLeft || movingRight)
			flipped = true;
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

	public virtual void ReverseGravity()
	{
		transform.up = -transform.up;
		rb.gravityScale = -rb.gravityScale;
		myRight = transform.right;
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
			StopMovingLeft();
			StopMovingRight();
			animator.speed = 0;
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

	protected virtual void Update()
	{

	}

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		collidings++;
		grounded = collidings > 0;
	}

	protected virtual void OnCollisionExit2D(Collision2D collision)
	{
		collidings--;
		grounded = collidings > 0;
	}

	protected void GetAudioPlayer()
	{
		ap = AudioManager.instance.GetPlayer("SFX");
		hasPlayer = true;
	}
	#endregion
}
