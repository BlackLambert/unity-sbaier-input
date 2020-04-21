using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public class ClickInputDetectorParameter
	{
		public ClickInputParameter InputParameter { get; }
		public ClickPointerParameter PointerParameter { get; }

		public ClickInputDetectorParameter(ClickInputParameter clickParameter,
			ClickPointerParameter pointerParameter)
		{
			InputParameter = clickParameter;
			PointerParameter = pointerParameter;
		}
	}
}