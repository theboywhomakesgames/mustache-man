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
	#endregion
}
