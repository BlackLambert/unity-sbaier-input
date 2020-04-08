using System;
using UnityEngine;

namespace SBaier.Input
{
	public abstract class ButtonInputDetector : InputDetector
	{
		public abstract KeyCode Key { get; }
		public abstract event Action<ButtonInputEventArgs> OnDown;
		public abstract event Action<ButtonInputEventArgs> OnPress;
		public abstract event Action<ButtonInputEventArgs> OnReleased;
	}
}