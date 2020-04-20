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
		}

		protected virtual void OnDisable()
		{
			_pointerButtonInputRegistry.Unsubscribe(_parameter.PointerButtonIndex, ButtonState.Down, onPointerButtonInputDown);
			_pointerButtonInputRegistry.Unsubscribe(_parameter.PointerButtonIndex, ButtonState.Released, onPointerButtonInputUp);
		}


		private void onPointerButtonInputDown(PointerButtonInputEventArgs args)
		{
			_pointerInputOnClickStart = _pointerInputRegistry.GetPointerInput(_parameter.PointerButtonIndex);
		}

		private void onPointerButtonInputUp(PointerButtonInputEventArgs args)
		{
			PointerInputEventArgs pointerInput = _pointerInputRegistry.GetPointerInput(_parameter.PointerButtonIndex);
			if (args.Duration > _parameter.ClickParameter.MaxClickDuration ||
				(pointerInput.ScreenPosition - _pointerInputOnClickStart.ScreenPosition).magnitude > _parameter.ClickParameter.MaxPointerDelta)
			{
				clean();
				return;
			}

			List<PointerRaycastHit> hits = new List<PointerRaycastHit>();

			foreach(PointerRaycastHit hit in pointerInput.RaycastHits)
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

		private void clean()
		{
			_pointerInputOnClickStart = null;
		}
	}
}