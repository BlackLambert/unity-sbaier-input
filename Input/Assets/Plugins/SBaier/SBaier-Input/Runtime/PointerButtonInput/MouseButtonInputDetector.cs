using System;
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class MouseButtonInputDetector : PointerButtonInputDetector
	{
		private int _index;
		private float _stateDuration = 0;

		[Inject]
		private void Construct(int index)
		{
			_index = index;
		}

		protected virtual void Update()
		{
			if (UnityEngine.Input.GetMouseButtonDown(_index))
				OnDown?.Invoke(createArg(ButtonState.Down));
			else if (UnityEngine.Input.GetMouseButtonUp(_index))
			{
				_stateDuration += Time.deltaTime;
				OnReleased?.Invoke(createArg(ButtonState.Released));
				_stateDuration = 0;
			}
			else if (UnityEngine.Input.GetMouseButton(_index))
			{
				_stateDuration += Time.deltaTime;
				OnPress?.Invoke(createArg(ButtonState.Pressed));
			}
		}

		private PointerButtonInputEventArgs createArg(ButtonState state)
		{
			return new PointerButtonInputEventArgs(_index, state, _stateDuration);
		}


		public override int Index => _index;

		public override event Action<PointerButtonInputEventArgs> OnDown;
		public override event Action<PointerButtonInputEventArgs> OnPress;
		public override event Action<PointerButtonInputEventArgs> OnReleased;


	}
}