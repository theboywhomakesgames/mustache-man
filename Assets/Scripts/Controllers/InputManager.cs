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
	public MainCharacter controller;
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
			controller.Move(1);
		}

		if (Input.GetKey(im.walkLeft.key))
		{
			controller.Move(-1);
		}

		if (Input.GetKeyDown(im.walkRight.key))
		{
			controller.StartMovingForward();
		}

		if (Input.GetKeyUp(im.walkRight.key))
		{
			controller.StopMovingForward();
		}

		if (Input.GetKeyDown(im.walkLeft.key))
		{
			controller.StartMovingBackward();
		}

		if (Input.GetKeyUp(im.walkLeft.key))
		{
			controller.StopMovingBackward();
		}

		if (Input.GetKeyDown(im.jump.key))
		{
			controller.Jump();
		}

		if (Input.GetKeyDown(im.slowMo.key))
		{
			controller.SlowMoSwitch();
		}

		if (Input.GetKeyDown(im.interact.key))
		{
			controller.InteractWithNearby();
		}

		if (Input.GetMouseButton(im.rightHandInteract.mouseButton))
		{
			controller.RightHandInteract();
		}
	}
	#endregion
}
