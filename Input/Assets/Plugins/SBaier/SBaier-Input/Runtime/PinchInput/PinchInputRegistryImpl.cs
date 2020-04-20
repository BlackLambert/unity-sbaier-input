using System;
using Zenject;

namespace SBaier.Input
{
	public class PinchInputRegistryImpl : PinchInputRegistry
	{
		private PinchInputDetector _detector = null;
		private PinchInputDetector _detectorPrefab;


		[Inject]
		private void Construct(PinchInputDetector detectorPrefab)
		{
			_detectorPrefab = detectorPrefab;
		}


		public override void Subscribe(PinchState state, Action<PinchInputEventArgs> listener)
		{
			switch(state)
			{
				case PinchState.Start:
					getDetector().OnPinchStart += listener;
					break;
				case PinchState.Ongoing:
					getDetector().OnPinch += listener;
					break;
				case PinchState.Ended:
					getDetector().OnPinchEnd += listener;
					break;
				case PinchState.None:
					throw new ArgumentException();
				default:
					throw new NotImplementedException();
			}
			
		}

		public override void Unsubscribe(PinchState state, Action<PinchInputEventArgs> listener)
		{
			if (_detector == null)
				return;

			switch (state)
			{
				case PinchState.Start:
					getDetector().OnPinchStart -= listener;
					break;
				case PinchState.Ongoing:
					getDetector().OnPinch -= listener;
					break;
				case PinchState.Ended:
					getDetector().OnPinchEnd -= listener;
					break;
				case PinchState.None:
					throw new ArgumentException();
				default:
					throw new NotImplementedException();
			}
		}

		private PinchInputDetector getDetector()
		{
			if (_detector != null)
				return _detector;
			PrefabFactory.Parameter[] parameters = new PrefabFactory.Parameter[] {  };
			_detector = createInputDetector(_detectorPrefab, parameters);
			return _detector;
		}
	}
}