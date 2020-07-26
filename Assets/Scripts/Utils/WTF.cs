using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WTF : MonoBehaviour
{
	[Header("WTF vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	//[Header("floats")]
	public float betweenFrames = 0.1f;
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	public Sprite[] animation_;
	public SpriteRenderer sr;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions

	public void Disable()
	{
		sr.enabled = false;
	}

	public void Play()
	{
		sr.enabled = true;
		StartCoroutine(PlayAnimation());
	}

	#endregion

	#region PrivateFunctions
	private void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		Disable();
	}

	private IEnumerator PlayAnimation()
	{
		foreach(Sprite s in animation_)
		{
			sr.sprite = s;
			yield return new WaitForSeconds(betweenFrames);
		}

		Invoke(nameof(Disable), 5);
	}
	#endregion
}
