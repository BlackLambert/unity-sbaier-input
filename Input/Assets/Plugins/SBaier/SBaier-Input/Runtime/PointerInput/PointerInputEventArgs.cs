using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public class PointerInputEventArgs 
	{
		public Vector2 ScreenPosition { get; }
		public Vector2 Delta { get; }
		public PointerRaycastHit[] RaycastHits { get; }

		public PointerInputEventArgs(Vector2 screenPosition,
			Vector2 delta,
			PointerRaycastHit[] raycastHits)
		{
			ScreenPosition = screenPosition;
			Delta = delta;
			RaycastHits = raycastHits;
		}
	}
}