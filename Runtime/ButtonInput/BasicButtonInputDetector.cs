using System;
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class BasicButtonInputDetector : ButtonInputDetector
	{
		private KeyCode _key;
		private float _stateDuration = 0;

		[Inject]
		private void Construct(KeyCode key)
		{
			_key = key;
		}

		protected virtual void Update()
		{
			if (UnityEngine.Input.GetKeyDown(_key))
			{
				OnDown?.Invoke(createArg(ButtonState.Down));
			}
			else if(UnityEngine.Input.GetKey(_key))
			{
				_stateDuration += Time.deltaTime;
				OnPress?.Invoke(createArg(ButtonState.Pressed));
			}
			else if(UnityEngine.Input.GetKeyUp(_key))
			{
				OnReleased?.Invoke(createArg(ButtonState.Released));
				_stateDuration = 0;
			}
		}

		private ButtonInputEventArgs createArg(ButtonState state)
		{
			return new ButtonInputEventArgs(_key, state, _stateDuration);
		}


		public override KeyCode Key => _key;

		public override event Action<ButtonInputEventArgs> OnDown;
		public override event Action<ButtonInputEventArgs> OnPress;
		public override event Action<ButtonInputEventArgs> OnReleased;
	}
}