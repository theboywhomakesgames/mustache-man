using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
	[Header("DestroyAfter vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	[Header("floats")]
	public float time = 2;
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
	public void DestroyIt()
	{
		Destroy(gameObject);
	}
	#endregion

	#region PrivateFunctions
	private void Start()
	{
		if(time >= 0)
			Destroy(gameObject, time);
	}
	#endregion
}
