using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SBaier.Input.Test
{
	public class TestButtonListener : MonoBehaviour
	{
		private ButtonInputRegistry _registry;
		[SerializeField]
		private KeyCode _testKey = KeyCode.A;

		[Inject]
		private void Construct(ButtonInputRegistry registry)
		{
			_registry = registry;
		}

		protected virtual void Start()
		{
			_registry.Subscribe(_testKey, ButtonState.Pressed, onButtonPress);
		}

		private void onButtonPress(ButtonInputEventArgs obj)
		{
			Debug.Log($"The {_testKey.ToString()}-Button has been pressed for {obj.Duration} seconds.");
		}

		protected virtual void OnDestroy()
		{
			_registry.Unsubscribe(_testKey, ButtonState.Pressed, onButtonPress);
		}
	}
}

