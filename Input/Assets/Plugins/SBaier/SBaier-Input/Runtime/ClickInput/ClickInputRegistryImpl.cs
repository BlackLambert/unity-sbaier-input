using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class ClickInputRegistryImpl : ClickInputRegistry
	{
		private Dictionary<Vector2, ClickInputDetector> _detectors = new Dictionary<Vector2, ClickInputDetector>();
		private ClickInputDetector _detectorPrefab;
		private ClickInputParameter _parameter;


		[Inject]
		private void Construct(ClickInputDetector detectorPrefab,
			ClickInputParameter parameter)
		{
			_detectorPrefab = detectorPrefab;
			_parameter = parameter;
		}

		public override void Subscribe(int pointerIndex, int pointerButtonIndex, Action<ClickInputEventArgs> listener)
		{
			ClickInputDetector detector = getDetector(pointerIndex, pointerButtonIndex);
			detector.OnClick += listener;
		}

		public override void Unsubscribe(int pointerIndex, int pointerButtonIndex, Action<ClickInputEventArgs> listener)
		{
			ClickInputDetector detector = getDetector(pointerIndex, pointerButtonIndex);
			detector.OnClick -= listener;
		}

		private ClickInputDetector getDetector(int pointerIndex, int pointerButtonIndex)
		{
			ClickInputDetector inputDetector;
			Vector2 key = new Vector2(pointerIndex, pointerButtonIndex);
			if (!_detectors.TryGetValue(key, out inputDetector))
			{
				ClickInputDetectorParameter detectorParameter = new ClickInputDetectorParameter(_parameter, pointerIndex, pointerButtonIndex);
				PrefabFactory.Parameter[] parameters = new PrefabFactory.Parameter[] { new PrefabFactory.Parameter(detectorParameter) };
				inputDetector = createInputDetector(_detectorPrefab, parameters);
				_detectors.Add(key, inputDetector);
			}
			return inputDetector;
		}
	}
}