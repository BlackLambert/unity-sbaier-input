
using System.Linq;
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public abstract class Selector<TSelectable> : MonoBehaviour where TSelectable : Selectable
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

		public void Deselect()
		{
			if (_selected != null)
				_selected.Deselect();
			_selected = null;
		}

		private void onClick(ClickInputEventArgs args)
		{
			if (args.ClickedObjects.Length == 0)
				return;
			PointerRaycastHit hit = args.ClickedObjects[0];
			TSelectable selectable = hit.Obj.GetComponent<TSelectable>();
			if (selectable == null)
				return;
			onSelectableClicked(createArgs(selectable, hit, args.PointerInput));
		}

		private void onSelectableClicked(SelectableInputEventArgs args)
		{
			if (!args.Selectable.IsSelectable)
				return;
			if (_selected == args.Selectable)
				onSelectableDoubleSelect(args);
			else
				onOtherSelectableClicked(args);
		}

		private void onSelectableDoubleSelect(SelectableInputEventArgs args)
		{
			if (args.Selectable.DeselectOnDoubleSelect)
			{
				_selected.Deselect();
				_selected = null;
			}
			if (args.Selectable.SelectAgainOnDoubleSelect)
			{
				_selected = args.Selectable;
				_selected.Select(args);
			}
		}

		private void onOtherSelectableClicked(SelectableInputEventArgs args)
		{
			if (_selected != null)
				_selected.Deselect();
			_selected = args.Selectable;
			_selected.Select(args);
		}

		private SelectableInputEventArgs createArgs(TSelectable selectable, PointerRaycastHit hit, PointerInputEventArgs args)
		{
			return new SelectableInputEventArgs(selectable, hit, args);
		}
	}
}