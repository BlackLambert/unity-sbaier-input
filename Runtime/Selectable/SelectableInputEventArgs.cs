using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public struct SelectableInputEventArgs
	{
		public PointerRaycastHit RaycastHit { get; }
		public PointerInputEventArgs PointerInput { get; }

		public SelectableInputEventArgs(PointerRaycastHit raycastHit,
			PointerInputEventArgs pointerInput)
		{
			RaycastHit = raycastHit;
			PointerInput = pointerInput;
		}
	}
}