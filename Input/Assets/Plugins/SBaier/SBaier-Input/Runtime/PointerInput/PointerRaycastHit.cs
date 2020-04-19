using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public struct PointerRaycastHit 
	{
		public Transform Obj { get; }
		public Vector3 Point { get; }
		public float Distance { get; }

		public PointerRaycastHit(Transform obj,
			Vector3 point, float distance)
		{
			Obj = obj;
			Point = point;
			Distance = distance;
		}
	}
}