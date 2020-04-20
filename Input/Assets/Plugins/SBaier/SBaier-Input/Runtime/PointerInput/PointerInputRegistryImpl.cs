using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class PointerInputRegistryImpl : PointerInputRegistry
	{
		private PointersInputDetector _detector;
		private PointersInputDetector _detectorPrefab;
		private int _maxPointerCount;
		private float _maxRaycastDistance;
		private float _lastCheckedTime;

		private PointersInputEventArgs _pointersInput;
		private PointersInputEventArgs PointersInput
		{
			get
			{
				if (_lastCheckedTime == Time.time)
					return _pointersInput;
				_lastCheckedTime = Time.time;
				_pointersInput = getDetector().GetPointerInputs();
				return _pointersInput;
			}
	}


		[Inject]
		private void Construct(PointersInputDetector detectorPrefab, int maxPointerCount,
			float maxRaycastDistance)
		{
			_detectorPrefab = detectorPrefab;
			_maxPointerCount = maxPointerCount;
			_maxRaycastDistance = maxRaycastDistance;
			_lastCheckedTime = -1;
		}


		public override PointerInputEventArgs GetPointerInput(int index)
		{
			if (index >= _maxPointerCount)
				throw new ArgumentException($"Adding the listener has failed. The maximal pointer count is set to {_maxPointerCount}.");

			if (PointersInput.Count <= index)
				return null;
			return PointersInput.PointerInputs[index];
		}

		public override PointersInputEventArgs GetPointerInput()
		{
			return PointersInput;
		}

		private PointersInputDetector getDetector()
		{
			if (_detector == null)
				_detector = createInputDetector(_detectorPrefab, new PrefabFactory.Parameter[] {
					new PrefabFactory.Parameter(_maxPointerCount), new PrefabFactory.Parameter(_maxRaycastDistance) });
			return _detector;
		}
	}
}
