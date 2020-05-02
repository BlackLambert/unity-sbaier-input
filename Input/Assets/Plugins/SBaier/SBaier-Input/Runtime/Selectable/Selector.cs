
using System.Linq;
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class Selector<TSelectable> : MonoBehaviour where TSelectable : Selectable
	{
		private ClickInputRegistry _clickInputRegistry;
		private ClickPointerParameter _clickPointerParameter;

		private Selectable _selected = null;

		[Inject]
		private void Construct(ClickInputRegistry clickInputRegistry, 
			ClickPointerParameter clickPointerParameter)
		{
			_clickInputRegistry = clickInputRegistry;
			_clickPointerParameter = clickPointerParameter;
		}

		protected virtual void Start()
		{
			_clickInputRegistry.Subscribe(_clickPointerParameter, onClick);
		}

		protected virtual void OnDestroy()
		{
			_clickInputRegistry.Unsubscribe(_clickPointerParameter, onClick);
		}

		private void onClick(ClickInputEventArgs args)
		{
			if (args.ClickedObjects.Length == 0)
				return;
			PointerRaycastHit hit = args.ClickedObjects[0];
			TSelectable selectable = hit.Obj.GetComponent<TSelectable>();
			if (selectable == null)
				return;
			if (!selectable.IsSelectable)
				return;
			if (_selected == selectable)
			{
				_selected.Deselect();
				_selected = null;
			}
			else
			{
				if (_selected != null)
					_selected.Deselect();
				_selected = selectable;
				_selected.Select(new SelectableInputEventArgs(hit, args.PointerInput));
			}
		}
	}
}