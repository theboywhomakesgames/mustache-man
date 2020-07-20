using Assets.Scripts.Object;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BasicGun : InteractableObj
{
	[Header("BasicGun vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	[Header("floats")]
	public float coolDown;
	[Header("ints")]
	public int clipSize = 10;
	//[Header("bools")]
	[Header("GO, Transforms")]
	public GameObject bulletPrefab;
	public Transform hole;
	public Sprite inHandSprite, droppedSprite;
	public SpriteRenderer sr;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	private float _time;
	//[Header("ints")]
	//[Header("bools")]
	private bool _isCoolingDown;
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions

	public override void InteractWith()
	{
		Shoot();
	}

	public void Shoot()
	{
		if (!_isCoolingDown)
		{
			_time = 0;
			_isCoolingDown = true;
			Vector2 diff = holder.target - (Vector2)holder.rightArm.position;
			diff = diff.normalized;
			GameObject blt = Instantiate(bulletPrefab, hole.position, Quaternion.FromToRotation(Vector3.right, diff));
			blt.GetComponent<SimpleBullet>().GetShot(diff);
		}
	}

	public void Rotate(Vector2 towards)
	{
		transform.right = towards;
	}

	public override void GetPickedUpBy(Person picker)
	{
		sr.sprite = inHandSprite;
		base.GetPickedUpBy(picker);
	}

	public override void GetDropped(Vector2 force)
	{
		sr.sprite = droppedSprite;
		base.GetDropped(force);
	}
	#endregion

	#region PrivateFunctions
	protected override void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		base.Start();
	}

	private void Update()
	{
		if (_isCoolingDown)
		{
			_time += Time.deltaTime;
			if(_time >= coolDown)
			{
				_time = 0;
				_isCoolingDown = false;
			}
		}
	}
	#endregion
}
