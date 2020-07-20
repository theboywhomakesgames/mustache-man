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

		private bool hasHolder = false;

		public virtual void InteractWith()
		{

		}

		public virtual void GetPickedUpBy(Person picker)
		{
			try
			{
				colliders.SetActive(false);
			}
			catch { }
			rb.bodyType = RigidbodyType2D.Kinematic;
			hasHolder = true;
			holder = picker;
			transform.parent = picker.righthandPos;
			transform.localPosition = Vector3.zero;
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
