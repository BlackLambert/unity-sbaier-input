using System;

namespace SBaier.Input
{
	public abstract class ClickInputRegistry : InputRegistry
	{
		public abstract void Subscribe(ClickPointerParameter parameter, Action<ClickInputEventArgs> listener);

		public abstract void Unsubscribe(ClickPointerParameter parameter, Action<ClickInputEventArgs> listener);
	}
}