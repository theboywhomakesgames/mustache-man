using Assets.Scripts.Object;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;

public class BasicGun : InteractableObj
{
	[Header("BasicGun vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	[Header("floats")]
	public float coolDown;
	public float shootingLightDuration = 0.1f;
	public float shootingSoundRadius = 4;
	public float betweenReloads = 3;
	[Header("ints")]
	public int clipSize = 10;
	public int remainingBullets = 0, magzLeft = 2;
	//[Header("bools")]
	[Header("GO, Transforms")]
	public GameObject bulletPrefab;
	public Transform hole;
	public Sprite inHandSprite, droppedSprite;
	public SpriteRenderer sr;
	public GameObject shootingLight;
	public AudioClip shootingSFX;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	private float _time;
	//[Header("ints")]
	//[Header("bools")]
	private bool _isCoolingDown;
	//[Header("GO, Transforms")]
	protected bool hasPlayer = false;
	protected AudioPlayer ap;
	#endregion

	#region PublicFunctions
	public void PlayShooingSFX()
	{
		if (!hasPlayer)
			GetAudioPlayer();

		ap.as_.PlayOneShot(shootingSFX);
	}

	public override void InteractWith()
	{
		Shoot();
	}

	public void SetUI()
	{
		if(isPlayerHeld)
			GunUI.SetTXT(remainingBullets, clipSize * magzLeft);
	}

	public void Reload()
	{
		Invoke(nameof(ResetClip), betweenReloads);
	}

	public void ResetClip()
	{
		remainingBullets = clipSize;
		magzLeft--;
		SetUI();
	}

	public void Shoot()
	{
		if (!_isCoolingDown && remainingBullets > 0)
		{
			_time = 0;
			_isCoolingDown = true;
			Vector2 diff = holder.target - (Vector2)holder.rightArm.position;
			diff = diff.normalized;
			GameObject blt = Instantiate(bulletPrefab, hole.position, Quaternion.FromToRotation(Vector3.right, diff));
			shootingLight.SetActive(true);
			Invoke(nameof(TurnOffLight), shootingLightDuration);
			PlayShooingSFX();
			MakeNoise();
			blt.GetComponent<SimpleBullet>().GetShot(diff);

			remainingBullets--;
			SetUI();
			if(remainingBullets <= 0)
			{
				Reload();
			}
		}
	}

	public void MakeNoise()
	{
		int flooridx = 0;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
		foreach (Collider2D c in colliders)
		{
			if(c.gameObject.layer == 15)
			{
				try
				{
					flooridx = c.GetComponent<Floor>().index;
				}
				catch { }
			}
		}

		colliders = Physics2D.OverlapCircleAll(transform.position, 5);
		foreach(Collider2D c in colliders)
		{
			if(c.gameObject.layer == 9)
			{
				EnemyController ec = c.GetComponent<EnemyController>();
				Place np = new Place(flooridx, transform.position);
				ec.Suspect(np);
			}
		}
	}

	public void Rotate(Vector2 towards)
	{
		transform.right = towards;
	}

	public override void GetPickedUpBy(Person picker, bool isPlayer = false)
	{
		sr.sprite = inHandSprite;
		base.GetPickedUpBy(picker, isPlayer);

		if (isPlayerHeld)
		{
			GunUI.SetGun(droppedSprite, gameObject.name);
			SetUI();
		}
	}

	public override void GetDropped(Vector2 force)
	{
		sr.sprite = droppedSprite;
		base.GetDropped(force);
		if (isPlayerHeld)
		{
			GunUI.SetGun(null, "nothing");
			GunUI.SetTXT(0, 0);
		}
	}
	#endregion

	#region PrivateFunctions
	protected override void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		base.Start();
	}

	private void TurnOffLight()
	{
		shootingLight.SetActive(false);
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
	protected void GetAudioPlayer()
	{
		ap = AudioManager.instance.GetPlayer("SFX");
		hasPlayer = true;
	}
	#endregion
}
