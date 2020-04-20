using System;
using UnityEngine;

namespace SBaier.Input
{
	public abstract class PointerButtonInputDetector : InputDetector
	{
		public abstract int Index { get; }
		public abstract event Action<PointerButtonInputEventArgs> OnDown;
		public abstract event Action<PointerButtonInputEventArgs> OnPress;
		public abstract event Action<PointerButtonInputEventArgs> OnReleased;
	}
}