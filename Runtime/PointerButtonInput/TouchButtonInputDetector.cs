using System;
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class TouchButtonInputDetector : PointerButtonInputDetector
	{
		private int _index;
		private float _stateDuration = 0;

		private int _currentTouchID = int.MinValue;

		[Inject]
		private void Construct(int index)
		{
			_index = index;
		}

		protected virtual void Update()
		{
			Touch[] touches = UnityEngine.Input.touches;
			if (touches.Length <= _index)
				return;

			Touch touch = touches[_index];
			if(touch.phase == TouchPhase.Began)
			{
				_currentTouchID = touch.fingerId;
				OnDown?.Invoke(createArg(ButtonState.Down));
			}
			else if(touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
			{
				_currentTouchID = int.MinValue;
				OnReleased?.Invoke(createArg(ButtonState.Released));
			}
			else if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
			{
				if (_currentTouchID != touch.fingerId)
					throw new ArgumentException("The touch iD change before entering Canceled or Ended state!");
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