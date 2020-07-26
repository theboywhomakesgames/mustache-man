using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
	[Header("AudioPlayer vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	[Header("GO, Transforms")]
	public AudioSource as_;
	public string playerTag;
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
		as_ = GetComponent<AudioSource>();
	}
	#endregion
}
