using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class TouchPointersInputDetector : PointersInputDetector
	{
		public override PointersInputEventArgs PointerInputs => _pointerInputs;
		private PointersInputEventArgs _pointerInputs = new PointersInputEventArgs(new List<PointerInputEventArgs>());
		private int _maxPointer;

		public override event Action<PointersInputEventArgs> OnPointerInputsUpdate;


		[Inject]
		private void Construct(int maxPointer)
		{
			_maxPointer = maxPointer;
		}


		protected virtual void Update()
		{
			List<PointerInputEventArgs> pointerInputs = new List<PointerInputEventArgs>();
			int border = Mathf.Min(_maxPointer, UnityEngine.Input.touchCount);
			for (int i = 0; i < border; i++)
			{
				Touch touch = UnityEngine.Input.touches[i];
				Vector2 touchPos = touch.position;
				RaycastAllResult result = raycastAt(touchPos);
				PointerInputEventArgs touchPointerInput = new PointerInputEventArgs(touchPos, touch.deltaPosition, result.Hits, result.IsOverUI);
				pointerInputs.Add(touchPointerInput);
			}
			_pointerInputs = new PointersInputEventArgs(pointerInputs);
			OnPointerInputsUpdate?.Invoke(PointerInputs);
		}
	}
}