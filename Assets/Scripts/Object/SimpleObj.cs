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
	protected virtual void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	#endregion
}
