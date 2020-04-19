using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public class MousePointerInputDetector : PointersInputDetector
	{
		public override PointersInputEventArgs PointerInputs => _pointerInputs;
		private PointersInputEventArgs _pointerInputs = new PointersInputEventArgs(new List<PointerInputEventArgs>());

		public override event Action<PointersInputEventArgs> OnPointerInputsUpdate;
		private Vector2 _formerPos;
		
		protected virtual void Start()
		{
			_formerPos = UnityEngine.Input.mousePosition;
		}

		protected virtual void Update()
		{
			
			Vector2 mousePos = UnityEngine.Input.mousePosition;
			Vector2 delta = mousePos - _formerPos;
			_formerPos = mousePos;
			RaycastAllResult result = raycastAt(mousePos);
			PointerInputEventArgs mousePointerInput = new PointerInputEventArgs(mousePos, delta, result.Hits, result.IsOverUI);
			_pointerInputs = new PointersInputEventArgs(new List<PointerInputEventArgs>() { mousePointerInput });
			OnPointerInputsUpdate?.Invoke(PointerInputs);
		}
	}
}