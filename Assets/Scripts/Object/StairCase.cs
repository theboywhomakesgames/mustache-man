using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCase : InteractiveObj
{
	#region StaticProperties
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	[Header("GO, Transforms")]
	public StairCase pair;

	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions
	public override void Interact(Person p)
	{
		p.transform.position = pair.transform.position;
	}
	#endregion

	#region PrivateFunctions
	#endregion
}
