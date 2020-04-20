using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public class MousePointerInputDetector : PointersInputDetector
	{
		private Vector2 _formerPos;
		
		protected virtual void Start()
		{
			_formerPos = UnityEngine.Input.mousePosition;
		}

		public override PointersInputEventArgs GetPointerInputs()
		{
			Vector2 mousePos = UnityEngine.Input.mousePosition;
			Vector2 delta = mousePos - _formerPos;
			_formerPos = mousePos;
			RaycastAllResult result = raycastAt(mousePos);
			PointerInputEventArgs mousePointerInput = new PointerInputEventArgs(mousePos, delta, result.Hits, result.IsOverUI);
			return new PointersInputEventArgs(new List<PointerInputEventArgs>() { mousePointerInput }); 
		}
	}
}