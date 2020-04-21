using System;
using UnityEngine;

namespace SBaier.Input
{
	public abstract class Selectable : MonoBehaviour
	{
		public bool Selected { get; private set; } = false;
		public bool IsSelectable
		{
			get { return _isSelectable; }
			set { _isSelectable = value; }
		}
		private bool _isSelectable = true;

		public event Action OnSelect;
		public event Action OnDeselect;

		public void Select()
		{
			if (!_isSelectable)
				return;
			OnSelect?.Invoke();
		}

		public void Deselect()
		{
			OnDeselect?.Invoke();
		}
	}
}