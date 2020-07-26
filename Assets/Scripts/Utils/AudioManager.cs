using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Header("AudioManager vars")]

	#region StaticProperties
	public static AudioManager instance;
	public static List<AudioPlayer> players;
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
	public AudioPlayer GetPlayer(string tag)
	{
		foreach(AudioPlayer ap in players)
		{
			if(ap.playerTag == tag)
			{
				return ap;
			}
		}
		
		return null;
	}
	#endregion

	#region PrivateFunctions
	private void Start()
	{
		instance = this;
		players = new List<AudioPlayer>(GetComponentsInChildren<AudioPlayer>());
	}
	#endregion
}
