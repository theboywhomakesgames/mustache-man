using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
	#region StaticProperties
	public delegate void OnCollideCB(Collision2D collision);
	public delegate void OnTriggedCB(Collider2D collision);
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	public bool isTrigger;
	//[Header("GO, Transforms")]
	public event OnCollideCB onEnter;
	public event OnCollideCB onExit;
	public event OnCollideCB onStay;

	public event OnTriggedCB onEnterT;
	public event OnTriggedCB onExitT;
	public event OnTriggedCB onStayT;
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
		if(!isTrigger)
			onEnter?.Invoke(collision);
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (!isTrigger)
			onExit?.Invoke(collision);
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (!isTrigger)
			onStay?.Invoke(collision);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (isTrigger)
			onEnterT?.Invoke(collision);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (isTrigger)
			onExitT?.Invoke(collision);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (isTrigger)
			onStayT?.Invoke(collision);
	}
	#endregion
}
