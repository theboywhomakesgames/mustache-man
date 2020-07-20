using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeTweaker : MonoBehaviour
{
	[Header("TimeTweaker vars")]

	#region StaticProperties
	public static bool isSlow;
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
	//[Header("GO, Transforms")]
	private Tweener curTweenerTS, curTweenerDT;
	#endregion

	#region PublicFunctions
	public void Switch()
	{
		try
		{
			KillTweeners();
		}
		catch { }

		if (isSlow)
		{
			NormalizeTS();
		}
		else
		{
			SlowTS();
		}
	}
	#endregion

	#region PrivateFunctions
	private void Start()
	{
		scaleRatio = 0.01f;
		Time.fixedDeltaTime = 0.01f;
		print(scaleRatio);
		NormalizeTS();
	}

	private void KillTweeners()
	{
		curTweenerTS.Kill();
		curTweenerDT.Kill();
	}

	private void SlowTS()
	{
		isSlow = true;
		curTweenerTS = DOTween.To(() => Time.deltaTime, (float x) => { Time.timeScale = x; }, sloMoFactor, transitionDuration).SetUpdate(true);
		curTweenerDT = DOTween.To(() => Time.fixedDeltaTime, (float x) => { Time.fixedDeltaTime = x; }, sloMoFactor * scaleRatio, transitionDuration).SetUpdate(true);
	}

	private void NormalizeTS()
	{
		isSlow = false;
		curTweenerTS = DOTween.To(() => Time.deltaTime, (float x) => { Time.timeScale = x; }, 1, transitionDuration).SetUpdate(true);
		curTweenerDT = DOTween.To(() => Time.fixedDeltaTime, (float x) => { Time.fixedDeltaTime = x; }, scaleRatio, transitionDuration).SetUpdate(true);
	}
	#endregion
}
