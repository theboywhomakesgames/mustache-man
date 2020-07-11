using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class SimpleBullet : SimpleObj
{
	[Header("SimpleBullet vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	[Header("floats")]
	public float shootSpeed = 20;
	//[Header("ints")]
	[Header("bools")]
	public bool testing = true;
	[Header("GO, Transforms")]
	public GameObject sparkPrefab;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions
	public void GetShot(Vector2 dir)
	{
		rb.velocity = dir.normalized * shootSpeed;
	}
	#endregion

	#region PrivateFunctions
	protected override void Start()
	{
		base.Start();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		ContactPoint2D contactPoint = collision.GetContact(0);
		Instantiate(sparkPrefab, transform.position - (Vector3)contactPoint.normal * 0.05f, Quaternion.FromToRotation(Vector3.left, contactPoint.normal));
		Destroy(gameObject);
	}
	#endregion
}