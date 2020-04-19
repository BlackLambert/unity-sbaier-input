using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public struct ClickInputParameter 
	{
		public float MaxClickDuration { get; }
		public float MaxPointerDelta { get; }

		public ClickInputParameter(float maxClickDuration,
			float maxPointerDelta)
		{
			MaxClickDuration = maxClickDuration;
			MaxPointerDelta = maxPointerDelta;
		}
	}
}