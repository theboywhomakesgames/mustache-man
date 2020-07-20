using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObj : MonoBehaviour
{
	#region StaticProperties
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions
	public abstract void Interact(Person p);
	#endregion

	#region PrivateFunctions
	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		try
		{
			collision.GetComponent<Person>().nearbyInteractives.Add(this);
		}
		catch { }
	}

	protected virtual void OnTriggerExit2D(Collider2D collision)
	{
		try
		{
			collision.GetComponent<Person>().nearbyInteractives.Remove(this);
		}
		catch { }
	}
	#endregion
}
