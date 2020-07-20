using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDrop : SimpleObj
{
	[Header("BloodDrop vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	[Header("floats")]
	public float speed;
	public float betweenSpawns = 0.5f;
	//[Header("ints")]
	//[Header("bools")]
	[Header("GO, Transforms")]
	public GameObject bloodGO;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	private float _time;
	//[Header("ints")]
	//[Header("bools")]
	private bool inSpawnZone = false;
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions
	public void Throw(Vector2 dir)
	{
		rb.velocity = dir.normalized * speed;
	}
	#endregion

	#region PrivateFunctions
	private void Update()
	{
		_time += Time.deltaTime;
		if(_time > betweenSpawns)
		{
			_time = 0;
			SpawnOne();
		}
	}

	private void SpawnOne()
	{
		Instantiate(bloodGO, transform.position, Quaternion.identity);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == 13)
		{
			inSpawnZone = true;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject.layer == 13)
		{
			inSpawnZone = false;
		}
	}
	#endregion
}
