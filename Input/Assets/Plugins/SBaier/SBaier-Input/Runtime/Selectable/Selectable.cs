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

		public abstract bool DeselectOnDoubleSelect { get; }
		public abstract bool SelectAgainOnDoubleSelect { get; }

		public event Action<SelectableInputEventArgs> OnSelect;
		public event Action OnDeselect;

		public void Select(SelectableInputEventArgs args)
		{
			if (!_isSelectable)
				return;
			OnSelect?.Invoke(args);
		}

		public void Deselect()
		{
			OnDeselect?.Invoke();
		}
	}
}