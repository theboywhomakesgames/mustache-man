using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Object
{
    public class InteractableObj : SimpleObj
    {
        public Person holder;
		public GameObject colliders;

		protected bool hasHolder = false;
		protected bool isPlayerHeld = false;

		public virtual void InteractWith()
		{

		}

		public virtual void GetPickedUpBy(Person picker, bool isPlayer = false)
		{
			try
			{
				colliders.SetActive(false);
			}
			catch { }
			rb.bodyType = RigidbodyType2D.Kinematic;

			picker.rightHandFull = true;
			picker.rightHandContaining = this;

			hasHolder = true;
			holder = picker;

			transform.parent = picker.righthandPos;
			transform.localPosition = Vector3.zero;
			transform.localScale = new Vector3(1, 1, 1);
			transform.localRotation = Quaternion.Euler(0, 0, 0);

			if (isPlayer)
			{
				isPlayerHeld = true;
			}
		}

		public virtual void GetDropped(Vector2 force)
		{
			try
			{
				colliders.SetActive(true);
			}
			catch { }
			holder.rightHandContaining = null;
			holder.rightHandFull = false;
			rb.bodyType = RigidbodyType2D.Dynamic;
			hasHolder = false;
			holder = null;
			transform.parent = null;
			rb.velocity = force;
		}
		protected override void Start()
		{
			base.Start();
		}
	}
}
