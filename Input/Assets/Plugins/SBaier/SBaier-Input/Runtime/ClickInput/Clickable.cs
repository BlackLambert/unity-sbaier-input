using System;
using UnityEngine;

namespace SBaier.Input
{
	public class Clickable : MonoBehaviour
	{
		public event Action<ClickableInputEventArgs> OnClick;

		public void Click(ClickableInputEventArgs args)
		{
			OnClick?.Invoke(args);
		}
	}

}
