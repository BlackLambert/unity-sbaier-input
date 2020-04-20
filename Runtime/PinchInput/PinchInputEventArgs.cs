using UnityEngine;

namespace SBaier.Input
{
	public class PinchInputEventArgs 
	{
		public PinchState State { get; }
		public Vector2 Axis { get; }
		public float Delta { get; }
		public PointerInputEventArgs[] PointerInput { get; }
		public float Duration { get; }

		public PinchInputEventArgs(
			PinchState state,
			Vector2 axis,
			float delta, 
			PointerInputEventArgs[] pointerInput,
			float duration)
		{
			State = state;
			Axis = axis;
			Delta = delta;
			PointerInput = pointerInput;
			Duration = duration;
		}
	}
}