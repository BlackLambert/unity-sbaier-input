using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public class PointersInputEventArgs 
	{
		public List<PointerInputEventArgs> PointerInputs { get; }
		public int Count { get { return PointerInputs.Count; } }

		public PointersInputEventArgs(List<PointerInputEventArgs> pointerInputs)
		{
			PointerInputs = pointerInputs;
		}
	}
}