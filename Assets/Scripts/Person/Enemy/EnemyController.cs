using DG.Tweening.Plugins.Core.PathCore;
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
	public bool dontShoot, targetFound, roaming;
	public bool interactiveNP;
	public bool go, pathfind, arrived = true;
	public bool postIsStart;
	//[Header("GO, Transforms")]
	public Floor myfloor;
	public Transform intruder;
	public Vector2 target;
	public Place nextPlace;
	public Place post;
	public List<Place> nextPlaces = new List<Place>();
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	private int roamDir = 0;
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

		arrived = false;
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
		if (self.isAlive)
		{
			if (!targetFound)
			{
				NoTargetChase();
			}
			else
			{
				ChargeTarget();
			}
		}
	}

	private void ChargeTarget()
	{
		Vector2 targetDiff = GetDiffToTarget(intruder.position);
		nextPlace.location = intruder.position;
		Chase(targetDiff);
		self.AimAt(targetDiff.normalized);
		if (!dontShoot)
		{
			Shoot();
		}
		else
		{
			Invoke(nameof(EnableShooting), 0.5f);
		}
	}

	private void NoTargetChase()
	{
		if (roaming)
		{
			if (!go && arrived)
			{
				if(nextPlaces.Count == 0)
				{
					Roam();
				}
				nextPlace = nextPlaces[0];
				nextPlaces.RemoveAt(0);
				pathfind = true;
			}
		}

		if (pathfind)
		{
			pathfind = false;
			GotoPlace();
		}

		if (go)
		{
			Vector2 targetDiff = GetDiffToTarget(target);
			self.AimAt(targetDiff.normalized);

			if (interactiveNP)
			{
				InteractiveChase(targetDiff);
			}
			else
			{
				if(!arrived)
					Chase(targetDiff);
			}
		}
	}

	private void Roam()
	{
		if(roamDir == 0)
			roamDir = Random.Range(0, 0.99f) > 0.5f ? -1 : 1;

		int floorIdx = myfloor.house.floors.IndexOf(myfloor);
		int nxtIndex = 0;

		if (myfloor.house.floors.Count > 1)
		{
			nxtIndex = myfloor.index + roamDir;
			floorIdx += roamDir;

			if (floorIdx >= myfloor.house.floors.Count)
			{
				floorIdx -= 2;
				nxtIndex = myfloor.house.floors[floorIdx].index;
				roamDir = -1;
			}
			else if (floorIdx < 0)
			{
				floorIdx += 1;
				nxtIndex = myfloor.house.floors[floorIdx].index;
				roamDir = 1;
			}

			nextPlaces.Add(new Place(nxtIndex, myfloor.house.floors[floorIdx].leftMost.position));
			nextPlaces.Add(new Place(nxtIndex, myfloor.house.floors[floorIdx].rightMost.position));
		}
		else
		{
			nextPlaces.Add(new Place(myfloor.index, myfloor.leftMost.position));
			nextPlaces.Add(new Place(myfloor.index, myfloor.rightMost.position));
		}

		pathfind = true;
		roaming = true;
		go = false;
	}

	private void EnableShooting()
	{
		dontShoot = false;
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
			arrived = true;
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
			dontShoot = true;
			Roam();
		}
	}
	#endregion
}
