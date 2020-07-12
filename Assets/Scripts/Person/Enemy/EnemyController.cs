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
	public float deathRadius;
	//[Header("ints")]
	[Header("bools")]
	public bool walkforwardTest;
	//[Header("GO, Transforms")]
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	private Person self;
	private Person target;
	#endregion

	#region PublicFunctions
	#endregion

	#region PrivateFunctions
	private void Start()
	{
		self = GetComponent<Person>();
		target = FindObjectOfType<MainCharacter>();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, senseRadius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, deathRadius);
		Gizmos.color = Color.white;
	}

	private void Update()
	{
		Vector2 targetDiff = target.transform.position - transform.position;
		float magnitude = targetDiff.magnitude;
		self.target = target.transform.position;

		if (Mathf.Abs(targetDiff.x) > deathRadius)
		{
			if (self.movingForward)
			{
				self.Move(1);
			}
			else
			{
				self.StartMovingForward();
			}
		}
		else
		{
			self.StopMovingForward();
		}

		self.AimAt(targetDiff.normalized);
		self.RightHandInteract();
	}
	#endregion
}
