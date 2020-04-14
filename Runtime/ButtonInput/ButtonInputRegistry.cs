using System;
using UnityEngine;

namespace SBaier.Input
{
	public abstract class ButtonInputRegistry : InputRegistry
	{

		public abstract void Subscribe(KeyCode code, ButtonState state, Action<ButtonInputEventArgs> listener);
		public abstract void Unsubscribe(KeyCode code, ButtonState state, Action<ButtonInputEventArgs> listener);
	}
}