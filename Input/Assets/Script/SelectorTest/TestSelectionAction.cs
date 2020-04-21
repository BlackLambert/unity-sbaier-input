using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input.Test
{
	public class TestSelectionAction : MonoBehaviour
	{
		[SerializeField]
		private Selectable _selectable = null;
		[SerializeField]
		private MeshRenderer _meshRenderer = null;

		[SerializeField]
		private Material _notSelected = null;
		[SerializeField]
		private Material _selected = null;

		protected virtual void Start()
		{
			_selectable.OnSelect += onSelect;
			_selectable.OnDeselect += onDeselect;
		}

		protected virtual void OnDestroy()
		{
			_selectable.OnSelect -= onSelect;
			_selectable.OnDeselect -= onDeselect;
		}

		private void onSelect()
		{
			_meshRenderer.material = _selected;
		}

		private void onDeselect()
		{
			_meshRenderer.material = _notSelected;
		}
	}
}