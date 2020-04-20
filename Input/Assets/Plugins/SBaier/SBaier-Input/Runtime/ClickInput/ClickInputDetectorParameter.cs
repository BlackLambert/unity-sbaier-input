using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public class ClickInputDetectorParameter
	{
		public ClickInputParameter ClickParameter { get; }
		public int PointerButtonIndex { get; }
		public int PointerIndex { get; }

		public ClickInputDetectorParameter(ClickInputParameter clickParameter,
			int mouseButtonIndex,
			int pointerIndex)
		{
			ClickParameter = clickParameter;
			PointerButtonIndex = mouseButtonIndex;
			PointerIndex = pointerIndex;
		}
	}
}