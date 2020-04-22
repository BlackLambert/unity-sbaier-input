using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class Clicker : MonoBehaviour
	{
		private ClickInputRegistry _clickInputRegistry;
		private ClickPointerParameter _clickPointerParameter;

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
			Clickable clickable = hit.Obj.GetComponent<Clickable>();
			if (clickable == null)
				return;
			clickable.Click(new ClickableInputEventArgs(hit, args.PointerInput));
		}
	}
}