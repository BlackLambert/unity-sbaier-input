using System;
using UnityEngine;

namespace SBaier.Input
{
	public abstract class ClickInputDetector : InputDetector
	{
		public abstract event Action<ClickInputEventArgs> OnClick;
	}
}