using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeTweaker))]
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
	TimeTweaker tt;
	#endregion

	#region PublicFunctions
	public void SlowMoSwitch()
	{
		tt.Switch();
	}
	#endregion

	#region PrivateFunctions
	protected override void Start()
	{
		base.Start();
		tt = GetComponent<TimeTweaker>();
	}

	protected override void Die()
	{

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
