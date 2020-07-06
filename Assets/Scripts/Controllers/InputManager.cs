using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	[Header("InputManager vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	[Header("GO, Transforms")]
	public Inputs im;
	public Person person;
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
	private void Update()
	{
		if (Input.GetKey(im.walkRight.key))
		{
			person.Move(1);
		}

		if (Input.GetKeyDown(im.walkRight.key))
		{
			person.StartMoving();
		}

		if (Input.GetKeyUp(im.walkRight.key))
		{
			person.StopMoving();
		}
	}
	#endregion
}
