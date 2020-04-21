using UnityEngine;
using Zenject;

namespace SBaier.Input.Test
{
	public class TestClickListener : MonoBehaviour
	{
		private ClickInputRegistry _registry;
		[SerializeField]
		private int _pointerIndex = 0;
		[SerializeField]
		private int _pointerButtonIndex = 0;

		private ClickPointerParameter _parameter;

		[Inject]
		private void Construct(ClickInputRegistry registry)
		{
			_registry = registry;
			_parameter = new ClickPointerParameter(_pointerIndex, _pointerButtonIndex);
		}

		protected virtual void Start()
		{
			_registry.Subscribe(_parameter, onClick);
		}

		private void onClick(ClickInputEventArgs obj)
		{
			string log = $"The {_parameter.PointerIndex.ToString()}-PointerButton has been clicked at position {obj.PointerInput.ScreenPosition} ({UnityEngine.Input.mousePosition}) over {obj.ClickedObjects.Length} Objects.";
			foreach(PointerRaycastHit hit in obj.ClickedObjects)
			{
				log += $" {hit.Obj.name}";

			}
			Debug.Log(log);
		}

		protected virtual void OnDestroy()
		{
			_registry.Unsubscribe(_parameter, onClick);
		}
	}
}

