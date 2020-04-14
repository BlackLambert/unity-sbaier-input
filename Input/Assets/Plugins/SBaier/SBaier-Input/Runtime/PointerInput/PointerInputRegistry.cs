using System;

namespace SBaier.Input
{
	public abstract class PointerInputRegistry : InputRegistry
	{
		public abstract void Subscribe(int index, Action<PointerInputEventArgs> listener);
		public abstract void Subscribe(Action<PointersInputEventArgs> listener);
		public abstract void Unsubscribe(int index, Action<PointerInputEventArgs> listener);
		public abstract void Unsubscribe(Action<PointersInputEventArgs> listener);
	}
}