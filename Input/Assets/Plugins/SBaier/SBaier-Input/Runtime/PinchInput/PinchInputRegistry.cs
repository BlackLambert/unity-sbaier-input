using System;

namespace SBaier.Input
{
	public abstract class PinchInputRegistry : InputRegistry
	{
		public abstract void Subscribe(PinchState state, Action<PinchInputEventArgs> listener);
		public abstract void Unsubscribe(PinchState state, Action<PinchInputEventArgs> listener);
	}
}