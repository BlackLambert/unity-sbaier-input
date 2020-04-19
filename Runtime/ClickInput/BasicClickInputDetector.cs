using System;
using System.Collections.Generic;
using Zenject;

namespace SBaier.Input
{
	public class BasicClickInputDetector : ClickInputDetector
	{
		private PointerInputRegistry _pointerInputRegistry;
		private PointerButtonInputRegistry _pointerButtonInputRegistry;
		private ClickInputDetectorParameter _parameter;

		private PointerInputEventArgs _currentPointerInput = null;
		private PointerInputEventArgs _pointerInputOnClickStart = null;

		public override event Action<ClickInputEventArgs> OnClick;

		[Inject]
		private void Construct(PointerInputRegistry pointerInputRegistry,
			PointerButtonInputRegistry pointerButtonInputRegistry,
			ClickInputDetectorParameter parameter)
		{
			_pointerInputRegistry = pointerInputRegistry;
			_pointerButtonInputRegistry = pointerButtonInputRegistry;
			_parameter = parameter;
		}

		protected virtual void OnEnable()
		{
			_pointerButtonInputRegistry.Subscribe(_parameter.PointerButtonIndex, ButtonState.Down, onPointerButtonInputDown);
			_pointerButtonInputRegistry.Subscribe(_parameter.PointerButtonIndex, ButtonState.Released, onPointerButtonInputUp);
			_pointerInputRegistry.Subscribe(_parameter.PointerIndex, onPointerInput);
		}

		protected virtual void OnDisable()
		{
			_pointerButtonInputRegistry.Unsubscribe(_parameter.PointerButtonIndex, ButtonState.Down, onPointerButtonInputDown);
			_pointerButtonInputRegistry.Unsubscribe(_parameter.PointerButtonIndex, ButtonState.Released, onPointerButtonInputUp);
			_pointerInputRegistry.Unsubscribe(_parameter.PointerIndex, onPointerInput);
		}


		private void onPointerButtonInputDown(PointerButtonInputEventArgs args)
		{
			_pointerInputOnClickStart = _currentPointerInput;
		}

		private void onPointerButtonInputUp(PointerButtonInputEventArgs args)
		{
			if(args.Duration > _parameter.ClickParameter.MaxClickDuration ||
				(_currentPointerInput.ScreenPosition - _pointerInputOnClickStart.ScreenPosition).magnitude > _parameter.ClickParameter.MaxPointerDelta)
			{
				clean();
				return;
			}

			List<PointerRaycastHit> hits = new List<PointerRaycastHit>();

			foreach(PointerRaycastHit hit in _currentPointerInput.RaycastHits)
			{
				foreach(PointerRaycastHit clickStartHit in _pointerInputOnClickStart.RaycastHits)
				{
					if (hit.Obj != clickStartHit.Obj)
						continue;
					hits.Add(clickStartHit);
				}
			}

			OnClick?.Invoke(new ClickInputEventArgs(hits.ToArray(), _pointerInputOnClickStart));
			clean();
		}

		private void onPointerInput(PointerInputEventArgs args)
		{
			_currentPointerInput = args;
		}

		private void clean()
		{
			_pointerInputOnClickStart = null;
		}
	}
}