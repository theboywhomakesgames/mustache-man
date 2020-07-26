using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour
{
	[Header("GunUI vars")]

	#region StaticProperties
	public static GunUI instance;
	private static bool hasInstance = false;

	public static void SetTXT(int remaining, int clipSize)
	{
		if (hasInstance)
		{
			instance.clipTxt.text = remaining + "/" + clipSize;
		}
		else
		{
			print("gun ui has no instance");
		}
	}

	public static void SetGun(Sprite gunImage, string title)
	{
		if (hasInstance)
		{
			instance.title.text = title;
			instance.image.sprite = gunImage;
		}
		else
		{
			print("gun ui has no instance");
		}
	}
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	public Image image;

	public Text title;
	public Text clipTxt;
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
		instance = this;
		hasInstance = true;
	}

	private void OnDestroy()
	{
		hasInstance = false;
		instance = null;
	}
	#endregion
}
