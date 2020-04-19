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

		[Inject]
		private void Construct(ClickInputRegistry registry)
		{
			_registry = registry;
		}

		protected virtual void Start()
		{
			_registry.Subscribe(_pointerIndex, _pointerButtonIndex, onClick);
		}

		private void onClick(ClickInputEventArgs obj)
		{
			string log = $"The {_pointerIndex.ToString()}-PointerButton has been clicked over {obj.ClickedObjects.Length} Objects.";
			foreach(PointerRaycastHit hit in obj.ClickedObjects)
			{
				log += $" {hit.Obj.name}";

			}
			Debug.Log(log);
		}

		protected virtual void OnDestroy()
		{
			_registry.Unsubscribe(_pointerIndex, _pointerButtonIndex, onClick);
		}
	}
}

