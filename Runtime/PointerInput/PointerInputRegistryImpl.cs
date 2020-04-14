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

		private Dictionary<int, List<Action<PointerInputEventArgs>>> _pointerListeners = new Dictionary<int, List<Action<PointerInputEventArgs>>>();


		[Inject]
		private void Construct(PointersInputDetector detectorPrefab, int maxPointerCount,
			float maxRaycastDistance)
		{
			_detectorPrefab = detectorPrefab;
			_maxPointerCount = maxPointerCount;
			_maxRaycastDistance = maxRaycastDistance;
		}


		public override void Subscribe(int index, Action<PointerInputEventArgs> listener)
		{
			if (index >= _maxPointerCount)
				throw new ArgumentException($"Adding the listener has failed. The maximal pointer count is set to {_maxPointerCount}.");

			getDetector();
			List<Action<PointerInputEventArgs>> listeners;
			if(!_pointerListeners.TryGetValue(index, out listeners))
			{
				listeners = new List<Action<PointerInputEventArgs>>();
				_pointerListeners[index] = new List<Action<PointerInputEventArgs>>();
			}
			listeners.Add(listener);
		}

		public override void Subscribe(Action<PointersInputEventArgs> listener)
		{
			PointersInputDetector detector = getDetector();
			detector.OnPointerInputsUpdate += listener;
		}

		public override void Unsubscribe(int index, Action<PointerInputEventArgs> listener)
		{
			if (!_pointerListeners.ContainsKey(index) || !_pointerListeners[index].Contains(listener))
				return;
			_pointerListeners[index].Remove(listener);
		}

		public override void Unsubscribe(Action<PointersInputEventArgs> listener)
		{
			if (_detector == null)
				return;
			_detector.OnPointerInputsUpdate -= listener;
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
