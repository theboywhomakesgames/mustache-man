using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BasicGun : SimpleObj
{
	[Header("BasicGun vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	[Header("floats")]
	public float coolDown;
	[Header("ints")]
	public int clipSize = 10;
	//[Header("bools")]
	[Header("GO, Transforms")]
	public GameObject bulletPrefab;
	public Transform hole;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions

	public override void InteractWith()
	{
		Shoot();
	}

	public void Shoot()
	{
		Vector2 diff = holder.target - (Vector2)transform.position;
		diff = diff.normalized;
		GameObject blt = Instantiate(bulletPrefab, hole.position, Quaternion.FromToRotation(Vector3.right, diff));
		blt.GetComponent<SimpleBullet>().GetShot(diff);
	}

	public void Rotate(Vector2 towards)
	{
		transform.right = towards;
	}
	#endregion

	#region PrivateFunctions
	protected override void Start()
	{
		base.Start();
	}
	#endregion
}
