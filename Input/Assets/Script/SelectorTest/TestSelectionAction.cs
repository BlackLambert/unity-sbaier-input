
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

		private void onSelect(SelectableInputEventArgs args)
		{
			_meshRenderer.material = _selected;
			Debug.Log($"Selected at {args.RaycastHit.Point}");
		}

		private void onDeselect()
		{
			_meshRenderer.material = _notSelected;
			Debug.Log($"Deselected");
		}
	}
}