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
			Vector2 diff = holder.target - (Vector2)transform.position;
			diff = diff.normalized;
			GameObject blt = Instantiate(bulletPrefab, hole.position, Quaternion.FromToRotation(Vector3.right, diff));
			blt.GetComponent<SimpleBullet>().GetShot(diff);
		}
	}

	public void Rotate(Vector2 towards)
	{
		transform.right = towards;
	}
	#endregion

	#region PrivateFunctions
	protected override void Start()
	{
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
