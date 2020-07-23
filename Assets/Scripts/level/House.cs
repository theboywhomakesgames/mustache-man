using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
	[Header("House vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	[Header("GO, Transforms")]
	public List<Floor> floors;
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
	private void Start()
	{
		foreach(Floor f in floors)
		{
			f.house = this;
		}
	}
	#endregion
}
