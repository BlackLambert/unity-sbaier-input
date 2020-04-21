using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input
{
	public class Clickable : MonoBehaviour
	{
		public event Action OnClick;

		public void Click()
		{
			OnClick?.Invoke();
		}
	}

}
