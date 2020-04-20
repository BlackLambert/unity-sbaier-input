using System;
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class BasicPinchInputDetector : PinchInputDetector
	{
		private PointerInputRegistry _pointerInputRegistry;
		private PointerButtonInputRegistry _pointerButtonInputRegistry;

		private PointerInputEventArgs[] _currentPointerInput = new PointerInputEventArgs[2];
		private PointerInputEventArgs[] _pointerInputsOnClickStart = new PointerInputEventArgs[2];
		private float _pinchDuration = 0;
		private bool Pinching { get { return _pointerInputsOnClickStart[0] != null && _pointerInputsOnClickStart[1] != null; } }

		public override event Action<PinchInputEventArgs> OnPinch;
		public override event Action<PinchInputEventArgs> OnPinchStart;
		public override event Action<PinchInputEventArgs> OnPinchEnd;

		[Inject]
		private void Construct(PointerInputRegistry pointerInputRegistry,
			PointerButtonInputRegistry pointerButtonInputRegistry)
		{
			_pointerInputRegistry = pointerInputRegistry;
			_pointerButtonInputRegistry = pointerButtonInputRegistry;
		}

		protected virtual void OnEnable()
		{

			_pointerButtonInputRegistry.Subscribe(0, ButtonState.Released, onPointerButtonInputUp);
			_pointerButtonInputRegistry.Subscribe(1, ButtonState.Down, onPointerButtonInputDown);
			_pointerButtonInputRegistry.Subscribe(1, ButtonState.Pressed, onPointerButtonInputPressed);
			_pointerButtonInputRegistry.Subscribe(1, ButtonState.Released, onPointerButtonInputUp);
		}

		protected virtual void OnDisable()
		{
			_pointerButtonInputRegistry.Unsubscribe(0, ButtonState.Released, onPointerButtonInputUp);
			_pointerButtonInputRegistry.Unsubscribe(1, ButtonState.Down, onPointerButtonInputDown);
			_pointerButtonInputRegistry.Unsubscribe(1, ButtonState.Pressed, onPointerButtonInputPressed);
			_pointerButtonInputRegistry.Unsubscribe(1, ButtonState.Released, onPointerButtonInputUp);
		}

		private void onPointerButtonInputDown(PointerButtonInputEventArgs args)
		{
			fetchCurrentPointerInput();
			_pointerInputsOnClickStart[0] = _pointerInputRegistry.GetPointerInput(0);
			_pointerInputsOnClickStart[1] = _pointerInputRegistry.GetPointerInput(1);
			OnPinchStart?.Invoke(new PinchInputEventArgs(PinchState.Start, calculateAxis(), 0, _pointerInputsOnClickStart, 0));
		}

		private void onPointerButtonInputPressed(PointerButtonInputEventArgs obj)
		{
			_pinchDuration += Time.deltaTime;
			fetchCurrentPointerInput();
			OnPinch?.Invoke(new PinchInputEventArgs(PinchState.Ongoing, calculateAxis(), calculateDelta(), _currentPointerInput, _pinchDuration));
		}

		private void onPointerButtonInputUp(PointerButtonInputEventArgs obj)
		{
			if (!Pinching)
				return;

			_pinchDuration += Time.deltaTime;
			fetchCurrentPointerInput();
			OnPinchEnd?.Invoke(new PinchInputEventArgs(PinchState.Ended, calculateAxis(), calculateDelta(), _currentPointerInput, _pinchDuration));
			clean();
		}


		private void clean()
		{
			_pointerInputsOnClickStart[0] = null;
			_pointerInputsOnClickStart[1] = null;
			_pinchDuration = 0;
		}

		private Vector2 calculateAxis()
		{
			Vector2 first = _currentPointerInput[0].ScreenPosition;
			Vector2 second = _currentPointerInput[1].ScreenPosition;
			return (second - first).normalized;
		}

		private float calculateDelta()
		{
			Vector2 first = _currentPointerInput[0].ScreenPosition;
			Vector2 second = _currentPointerInput[1].ScreenPosition;
			Vector2 current = second - first;
			Vector2 former = _pointerInputsOnClickStart[1].ScreenPosition - _pointerInputsOnClickStart[0].ScreenPosition;
			return current.magnitude - former.magnitude;
		}

		private void fetchCurrentPointerInput()
		{
			_currentPointerInput[0] = _pointerInputRegistry.GetPointerInput(0);
			_currentPointerInput[1] = _pointerInputRegistry.GetPointerInput(1);
		}
	}
}