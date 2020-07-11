using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Person
{
	[Header("MainCharacter vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	[Header("bools")]
	public bool nothing;
	//[Header("GO, Transforms")]
	public Transform indicator;
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
	protected override void Start()
	{
		base.Start();
	}

	private void Update()
	{
		AimAtMouse();
	}

	private void AimAtMouse()
	{
		Camera cam = CameraManager.cams[0];
		Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
		Vector3 diff = mousePos - CameraManager.cams[0].transform.position;
		mousePos -= diff * 2;
		mousePos.z = 0;
		indicator.position = mousePos;
		target = mousePos;

		diff = indicator.position - rightArm.position;

		AimAt(diff);
	}
	#endregion
}
