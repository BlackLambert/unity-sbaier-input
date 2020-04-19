using System;
using UnityEngine;

namespace SBaier.Input
{
	public abstract class PointerButtonInputRegistry : InputRegistry
	{

		public abstract void Subscribe(int index, ButtonState state, Action<PointerButtonInputEventArgs> listener);
		public abstract void Unsubscribe(int index, ButtonState state, Action<PointerButtonInputEventArgs> listener);
	}
}