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
	public float damage = 20;
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
	private Vector2 vel;
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
		try
		{
			CollisionEvent ce = GetComponentInChildren<CollisionEvent>();
			ce.onEnter += OnCollisionEnterCB;
		}
		catch { }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		int layer = collision.gameObject.layer;
		if (layer == 9 || layer == 10)
		{
			try
			{
				collision.gameObject.GetComponent<Person>().Damage(damage, transform.position.x, transform.position.y, rb.velocity.x, rb.velocity.y);
			}
			catch { }
		}
	}

	private void OnCollisionEnterCB(Collision2D collision)
	{
		ContactPoint2D contactPoint = collision.GetContact(0);
		Instantiate(sparkPrefab, transform.position - (Vector3)contactPoint.normal * 0.05f, Quaternion.FromToRotation(Vector3.left, contactPoint.normal));
		Destroy(gameObject);
	}
	#endregion
}