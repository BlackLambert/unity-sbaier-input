using System;
using UnityEngine;

namespace SBaier.Input
{
	public abstract class PinchInputDetector : InputDetector
	{
		public abstract event Action<PinchInputEventArgs> OnPinchStart;
		public abstract event Action<PinchInputEventArgs> OnPinch;
		public abstract event Action<PinchInputEventArgs> OnPinchEnd;
	}
}