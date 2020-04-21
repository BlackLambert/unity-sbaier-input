using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public struct ClickPointerParameter 
	{
		public int PointerIndex { get; }
		public int PointerButtonIndex { get; }

		public ClickPointerParameter(int pointerIndex,
			int pointerButtonIndex)
		{
			PointerIndex = pointerIndex;
			PointerButtonIndex = pointerButtonIndex;
		}
	}
}