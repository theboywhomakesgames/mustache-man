using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveObj
{
	[Header("Door vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	public bool isOpen;
	//[Header("GO, Transforms")]
	public GameObject collider_;
	public SpriteRenderer sr;
	public Sprite openSprite, closeSprite;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions
	public override void Interact(Person p)
	{
		if (isOpen)
		{
			isOpen = false;
			sr.sprite = closeSprite;
			collider_.SetActive(true);
		}
		else
		{
			isOpen = true;
			sr.sprite = openSprite;
			collider_.SetActive(false);
		}
	}
	#endregion

	#region PrivateFunctions
	#endregion
}
