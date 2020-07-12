using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeTweaker : MonoBehaviour
{
	[Header("TimeTweaker vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	[Header("floats")]
	public float sloMoFactor = 0.1f;
	public float transitionDuration = 0.5f;
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PrivateVars
	//[Header("floats")]
	private float scaleRatio;
	//[Header("ints")]
	//[Header("bools")]
	private bool isSlow;
	//[Header("GO, Transforms")]
	private Tweener curTweenerTS, curTweenerDT;
	#endregion

	#region PublicFunctions
	public void Switch()
	{
		try
		{
			curTweenerTS.Kill();
			curTweenerDT.Kill();
		}
		catch { }

		if (isSlow)
		{
			curTweenerTS = DOTween.To(() => Time.deltaTime, (float x) => { Time.timeScale = x; }, 1, transitionDuration).SetUpdate(true);
			curTweenerDT = DOTween.To(() => Time.fixedDeltaTime, (float x) => { Time.fixedDeltaTime = x; }, scaleRatio, transitionDuration).SetUpdate(true);

			isSlow = false;
		}
		else
		{
			curTweenerTS = DOTween.To(() => Time.deltaTime, (float x) => { Time.timeScale = x; }, sloMoFactor, transitionDuration).SetUpdate(true);
			curTweenerDT = DOTween.To(() => Time.fixedDeltaTime, (float x) => { Time.fixedDeltaTime = x; }, sloMoFactor * scaleRatio, transitionDuration).SetUpdate(true);

			isSlow = true;
		}
	}
	#endregion

	#region PrivateFunctions
	private void Start()
	{
		ObjectHolder.Instance.dictionary.Add("tt", this);
		scaleRatio = Time.fixedDeltaTime / Time.timeScale;
		print(scaleRatio);
	}
	#endregion
}
