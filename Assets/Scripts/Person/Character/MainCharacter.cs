using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TimeTweaker))]
public class MainCharacter : Person
{
	[Header("MainCharacter vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	//[Header("floats")]
	public float slideDuration;
	public float slideOffsetY = 0.2f;
	//[Header("ints")]
	[Header("bools")]
	public bool nothing;
	//[Header("GO, Transforms")]
	public Transform indicator;
	public CapsuleCollider2D capsuleCollider;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	private float defYSize = 0;
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	TimeTweaker tt;
	#endregion

	#region PublicFunctions
	public void SlowMoSwitch()
	{
		tt.Switch();
	}

	public override void Slide()
	{
		int dir = movingRight ? 1 : movingLeft ? -1 : 0;
		if (dir != 0 && !isSliding)
		{
			isSliding = true;
			rb.velocity += new Vector2(dir * 2 * moveSpeed, 0);
			animator.SetBool("Sliding", true);

			defYSize = capsuleCollider.size.y;
			capsuleCollider.size = new Vector2(capsuleCollider.size.x * 3, 0.2f);
			DOTween.To(() => capsuleCollider.offset, (x) => { capsuleCollider.offset = x; }, new Vector2(capsuleCollider.offset.x, capsuleCollider.offset.y - slideOffsetY), slideDuration/2);

			Invoke(nameof(EndSlide), slideDuration);
		}
	}

	public void EndSlide()
	{
		isSliding = false;
		DOTween.To(() => capsuleCollider.size, (x) => { capsuleCollider.size = x; }, new Vector2(capsuleCollider.size.x / 3, defYSize), 0.5f);
		DOTween.To(() => capsuleCollider.offset, (x) => { capsuleCollider.offset = x; }, new Vector2(capsuleCollider.offset.x, capsuleCollider.offset.y + slideOffsetY), slideDuration / 2);
		animator.SetBool("Sliding", false);
	}
	#endregion

	#region PrivateFunctions
	protected override void Start()
	{
		base.Start();
		tt = GetComponent<TimeTweaker>();
	}

	protected override void Die()
	{
		if (isAlive)
		{
			Invoke(nameof(Restart), 2f);

			isAlive = false;
			if (rightHandFull)
			{
				rightHandContaining.GetDropped(new Vector2(UnityEngine.Random.Range(-1, 1f), UnityEngine.Random.Range(-1, 1f)).normalized);
			}

			rb.constraints = RigidbodyConstraints2D.None;
			Instantiate(bloodDropPref, lastAssualtPos, Quaternion.identity).GetComponent<BloodDrop>().Throw(lastAssualtDir);
			Instantiate(bloodPSPref, lastAssualtPos, Quaternion.identity);
			Vector2 dir = ((Vector2)transform.position - lastAssualtPos).normalized;
			rb.AddForceAtPosition(lastAssualtDir * 1000 * deathKick * 0.01f / Time.fixedDeltaTime, lastAssualtPos);
			StopMovingLeft();
			StopMovingRight();
			animator.speed = 0;

			try
			{
				onDeath.Excecute();
			}
			catch { }
		}
	}

	protected void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		base.OnCollisionEnter2D(collision);
	}

	protected override void OnCollisionExit2D(Collision2D collision)
	{
		base.OnCollisionExit2D(collision);
	}

	protected override void Update()
	{
		base.Update();
		AimAtMouse();
	}

	private void AimAtMouse()
	{
		Camera cam = CameraManager.cams[0];
		Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
		Vector3 diff = mousePos - CameraManager.cams[0].transform.position;
		mousePos -= diff * 2;
		mousePos.z = 0;
		indicator.position = mousePos;
		target = mousePos;

		diff = indicator.position - rightArm.position;

		AimAt(diff);
	}
	#endregion
}
