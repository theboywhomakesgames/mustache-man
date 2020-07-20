using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationFrames
{
	public Sprite[] sprites;
}

public class BloodSplatter : MonoBehaviour
{
	[Header("BloodSplatter vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	[Header("floats")]
	public float betweenFrames;
	//[Header("ints")]
	//[Header("bools")]
	[Header("GO, Transforms")]
	public SpriteRenderer sr;
	[SerializeField]
	public AnimationFrames[] animations;
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
	private IEnumerator PlayAnimation(int index = 0)
	{
		for(int i = 0; i < animations[index].sprites.Length; i++)
		{
			sr.sprite = animations[index].sprites[i];
			yield return new WaitForSeconds(betweenFrames);
		}
	}

	private void Start()
	{
		StartCoroutine(PlayAnimation(Random.Range(0, animations.Length)));
	}
	#endregion
}
