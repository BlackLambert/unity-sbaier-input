using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class TouchPointersInputDetector : PointersInputDetector
	{
		private int _maxPointer;


		[Inject]
		private void Construct(int maxPointer)
		{
			_maxPointer = maxPointer;
		}

		public override PointersInputEventArgs GetPointerInputs()
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
			return new PointersInputEventArgs(pointerInputs);
		}
	}
}