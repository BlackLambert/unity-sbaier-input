using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public class PointerInputEventArgs 
	{
		public Vector2 ScreenPosition { get; }
		public Vector2 Delta { get; }
		public RaycastHit[] RaycastHits { get; }

		public PointerInputEventArgs(Vector2 screenPosition,
			Vector2 delta,
			RaycastHit[] raycastHits)
		{
			ScreenPosition = screenPosition;
			Delta = delta;
			RaycastHits = raycastHits;
		}
	}
}