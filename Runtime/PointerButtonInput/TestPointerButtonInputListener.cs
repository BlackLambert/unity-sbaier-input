using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SBaier.Input.Test
{
	public class TestPointerButtonInputListener : MonoBehaviour
	{
		private PointerButtonInputRegistry _registry;
		[SerializeField]
		private int _index = 0;

		[Inject]
		private void Construct(PointerButtonInputRegistry registry)
		{
			_registry = registry;
		}

		protected virtual void Start()
		{
			_registry.Subscribe(_index, ButtonState.Pressed, onButtonPress);
		}

		private void onButtonPress(PointerButtonInputEventArgs obj)
		{
			Debug.Log($"The {_index.ToString()}-Button has been pressed for {obj.Duration} seconds.");
		}

		protected virtual void OnDestroy()
		{
			_registry.Unsubscribe(_index, ButtonState.Pressed, onButtonPress);
		}
	}
}