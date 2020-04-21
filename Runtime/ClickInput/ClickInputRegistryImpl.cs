using System;
using System.Collections.Generic;
using Zenject;

namespace SBaier.Input
{
	public class ClickInputRegistryImpl : ClickInputRegistry
	{
		private Dictionary<ClickPointerParameter, ClickInputDetector> _detectors = new Dictionary<ClickPointerParameter, ClickInputDetector>();
		private ClickInputDetector _detectorPrefab;
		private ClickInputParameter _inputParameter;


		[Inject]
		private void Construct(ClickInputDetector detectorPrefab,
			ClickInputParameter inputParameter)
		{
			_detectorPrefab = detectorPrefab;
			_inputParameter = inputParameter;
		}

		public override void Subscribe(ClickPointerParameter parameter, Action<ClickInputEventArgs> listener)
		{
			ClickInputDetector detector = getDetector(parameter);
			detector.OnClick += listener;
		}

		public override void Unsubscribe(ClickPointerParameter parameter, Action<ClickInputEventArgs> listener)
		{
			ClickInputDetector detector = getDetector(parameter);
			detector.OnClick -= listener;
		}

		private ClickInputDetector getDetector(ClickPointerParameter parameter)
		{
			ClickInputDetector inputDetector;
			if (!_detectors.TryGetValue(parameter, out inputDetector))
			{
				ClickInputDetectorParameter detectorParameter = new ClickInputDetectorParameter(_inputParameter, parameter);
				PrefabFactory.Parameter[] parameters = new PrefabFactory.Parameter[] { new PrefabFactory.Parameter(detectorParameter) };
				inputDetector = createInputDetector(_detectorPrefab, parameters);
				_detectors.Add(parameter, inputDetector);
			}
			return inputDetector;
		}
	}
}