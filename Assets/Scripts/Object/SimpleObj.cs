using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleObj : MonoBehaviour
{
	[Header("SimpleObj vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	[Header("GO, Transforms")]
	public Rigidbody2D rb;
	public Person holder;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	private bool hasHolder = false;
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions
	public virtual void InteractWith()
	{

	}

	public virtual void GetPickedUpBy(Person picker)
	{
		rb.bodyType = RigidbodyType2D.Kinematic;
		hasHolder = true;
		holder = picker;
		transform.parent = picker.righthandPos;
		transform.localPosition = Vector3.zero;
	}

	public virtual void GetDropped(Vector2 force)
	{
		rb.bodyType = RigidbodyType2D.Dynamic;
		hasHolder = false;
		holder = null;
		transform.parent = null;
		rb.velocity = force;
	}
	#endregion

	#region PrivateFunctions
	protected virtual void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	#endregion
}