using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

[RequireComponent(typeof(Person))]
public class EnemyController : MonoBehaviour
{
	[Header("EnemyController vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	[Header("floats")]
	public float senseRadius;
	public float interactRadius;
	//[Header("ints")]
	[Header("bools")]
	public bool walkforwardTest;
	public bool dontShoot, targetFound;
	public bool interactiveNP;
	public bool go, pathfind;
	public bool postIsStart;
	//[Header("GO, Transforms")]
	public Floor myfloor;
	public Transform intruder;
	public Vector2 target;
	public Place nextPlace;
	public Place post;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	private Person self;
	#endregion

	#region PublicFunctions
	public void GotoPlace()
	{
		if(nextPlace.flooridx == myfloor.index)
		{
			target = nextPlace.location;
			go = true;
		}
		else
		{
			Transform target_;
			int diff = nextPlace.flooridx - myfloor.index;
			target_ = diff > 0 ? myfloor.go_up_stairs.transform : myfloor.go_down_stairs.transform;
			target = target_.position;
			interactiveNP = true;
			go = true;
		}
	}
	#endregion

	#region PrivateFunctions
	private void Start()
	{
		self = GetComponent<Person>();
		Invoke(nameof(SetPost), 1);

		CollisionEvent ce = GetComponentInChildren<CollisionEvent>();
		ce.onEnterT += TriggerEnterCB;
		ce.onExitT += TriggerExitCB;
	}

	private void SetPost()
	{
		if (postIsStart)
		{
			post = new Place(myfloor.index, transform.position);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, senseRadius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, interactRadius);
		Gizmos.color = Color.white;
	}

	private void Update()
	{
		if (!targetFound)
		{
			if (pathfind)
			{
				pathfind = false;
				GotoPlace();
			}

			if (go)
			{
				Vector2 targetDiff = GetDiffToTarget(target);

				if (interactiveNP)
				{
					InteractiveChase(targetDiff);
				}
				else
				{
					Chase(targetDiff);
				}
			}
		}
		else
		{
			Vector2 targetDiff = GetDiffToTarget(intruder.position);
			nextPlace.location = intruder.position;
			Chase(targetDiff);
			self.AimAt(targetDiff.normalized);
			Shoot();
		}
	}

	private void Shoot()
	{
		self.RightHandInteract();
	}

	private Vector2 GetDiffToTarget(Vector2 target)
	{
		Vector2 targetDiff = target - (Vector2)transform.position;
		self.target = target;
		return targetDiff;
	}

	private void Chase(Vector2 targetDiff)
	{
		if (Mathf.Abs(targetDiff.x) > senseRadius)
		{
			if (targetDiff.x > 0)
			{
				if (self.movingRight)
				{
					self.Move(1);
				}
				else
				{
					self.StartMovingRight();
				}
			}
			else
			{
				if (self.movingLeft)
				{
					self.Move(-1);
				}
				else
				{
					self.StartMovingLeft();
				}
			}
		}
		else
		{
			self.StopMovingRight();
			self.StopMovingLeft();
			go = false;
		}
	}

	private void InteractiveChase(Vector2 targetDiff)
	{
		if (Mathf.Abs(targetDiff.x) > interactRadius)
		{
			if (targetDiff.x > 0)
			{
				if (self.movingRight)
				{
					self.Move(1);
				}
				else
				{
					self.StartMovingRight();
				}
			}
			else
			{
				if (self.movingLeft)
				{
					self.Move(-1);
				}
				else
				{
					self.StartMovingLeft();
				}
			}
		}
		else
		{
			self.StopMovingRight();
			self.StopMovingLeft();
			self.InteractWithNearby();
			interactiveNP = false;
			go = false;
			Invoke(nameof(GotoPlace), 0.1f);
		}
	}

	private void TriggerEnterCB(Collider2D collision)
	{
		if (collision.gameObject.layer == 10)
		{
			targetFound = true;
			nextPlace = new Place(myfloor.index, collision.transform.position);
			intruder = collision.transform;
		}
	}

	private void TriggerExitCB(Collider2D collision)
	{
		if (collision.gameObject.layer == 10)
		{
			targetFound = false;
			GotoPlace();
		}
	}
	#endregion
}
