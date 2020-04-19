using UnityEngine;

namespace SBaier.Input
{
	public class ClickInputEventArgs 
	{
		public PointerRaycastHit[] ClickedObjects { get; }
		public PointerInputEventArgs PointerInput { get; }

		public ClickInputEventArgs(PointerRaycastHit[] clickedObjects,
			PointerInputEventArgs pointerInput)
		{
			ClickedObjects = clickedObjects;
			PointerInput = pointerInput;
		}
	}
}