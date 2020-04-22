using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public struct ClickableInputEventArgs 
	{
		public PointerRaycastHit RaycastHit { get; }
		public PointerInputEventArgs PointerInput { get; }

		public ClickableInputEventArgs(PointerRaycastHit raycastHit,
			PointerInputEventArgs pointerInput)
		{
			RaycastHit = raycastHit;
			PointerInput = pointerInput;
		}
	}
}