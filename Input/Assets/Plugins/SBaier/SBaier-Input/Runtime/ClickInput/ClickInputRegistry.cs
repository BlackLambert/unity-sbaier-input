using System;

namespace SBaier.Input
{
	public abstract class ClickInputRegistry : InputRegistry
	{
		public abstract void Subscribe(int pointerIndex, int pointerButtonIndex , Action<ClickInputEventArgs> listener);
		public void Subscribe(int pointerIndex, Action<ClickInputEventArgs> listener)
		{
			Subscribe(pointerIndex, pointerIndex, listener);
		}
		public abstract void Unsubscribe(int pointerIndex, int pointerButtonIndex, Action<ClickInputEventArgs> listener);
		public void Unsubscribe(int pointerIndex, Action<ClickInputEventArgs> listener)
		{
			Unsubscribe(pointerIndex, pointerIndex, listener);
		}
	}
}