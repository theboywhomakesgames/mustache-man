using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetFollower : MonoBehaviour
{
	[Header("TargetFollower vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	[Header("floats")]
	public float width;
	public float height;
	public float duration;
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	public Transform target;
	public Vector2 offset;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctionsا 
	#endregion

	#region PrivateFunctions
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}

	private void Update()
	{
		Vector3 diff = target.position - transform.position;
		diff.z = 0;
		diff.x = Mathf.Abs(diff.x);
		diff.y = Mathf.Abs(diff.y);

		if(diff.x > width/2 || diff.y > height / 2)
		{
			transform.DOMove(new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z), duration);
		}
	}
	#endregion
}
