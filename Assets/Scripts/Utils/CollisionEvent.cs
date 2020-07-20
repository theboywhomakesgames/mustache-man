using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
	#region StaticProperties
	public delegate void OnCollideCB(Collision2D collision);
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	public event OnCollideCB onEnter;
	public event OnCollideCB onExit;
	public event OnCollideCB onStay;
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
	private void OnCollisionEnter2D(Collision2D collision)
	{
		onEnter?.Invoke(collision);
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		onExit?.Invoke(collision);
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		onStay?.Invoke(collision);
	}
	#endregion
}
