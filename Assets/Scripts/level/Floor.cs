using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Place
{
	public int flooridx;
	public Vector2 location;

	public Place(int floor_index, Vector2 floor_location)
	{
		flooridx = floor_index;
		location = floor_location;
	}
}

public class Floor : MonoBehaviour
{
	[Header("Floor vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	public int index;
	//[Header("bools")]
	//[Header("GO, Transforms")]
	public StairCase go_up_stairs;
	public StairCase go_down_stairs;

	public House house;

	public Transform leftMost, rightMost;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions
	#endregion

	#region PrivateFunctions
	private void OnTriggerEnter2D(Collider2D collision)
	{
		int layer = collision.gameObject.layer;
		if (layer == 9)
		{
			collision.GetComponent<EnemyController>().myfloor = this;
		}
	}
	#endregion
}
