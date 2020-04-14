using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public abstract class PointersInputDetector : InputDetector
	{
		public abstract PointersInputEventArgs PointerInputs { get; }
		public abstract event Action<PointersInputEventArgs> OnPointerInputsUpdate;
		private float _maxRaycastDistance;


		[Inject]
		private void Construct(float maxRaycastDistance)
		{
			_maxRaycastDistance = maxRaycastDistance;
		}
		

		protected RaycastHit[] raycastAt(Vector2 screenPosition)
		{
			if (Camera.current == null)
				return new RaycastHit[0];
			Ray ray = Camera.current.ScreenPointToRay(screenPosition);
			return Physics.RaycastAll(ray, _maxRaycastDistance);
		}
	}
}