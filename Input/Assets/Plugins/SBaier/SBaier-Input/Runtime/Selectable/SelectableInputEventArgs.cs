using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public struct SelectableInputEventArgs
	{
		public Selectable Selectable { get; }
		public PointerRaycastHit RaycastHit { get; }
		public PointerInputEventArgs PointerInput { get; }

		public SelectableInputEventArgs(
			Selectable selectable,
			PointerRaycastHit raycastHit,
			PointerInputEventArgs pointerInput)
		{
			Selectable = selectable;
			RaycastHit = raycastHit;
			PointerInput = pointerInput;
		}
	}
}