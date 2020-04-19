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

			PointersInputDetector detector = getDetector();
			if (_pointerListeners.Count == 0)
				detector.OnPointerInputsUpdate += onUpdate;

			if(!_pointerListeners.ContainsKey(index))
				_pointerListeners[index] = new List<Action<PointerInputEventArgs>>();
			_pointerListeners[index].Add(listener);
		}

		public override void Subscribe(Action<PointersInputEventArgs> listener)
		{
			PointersInputDetector detector = getDetector();
			detector.OnPointerInputsUpdate += listener;
		}

		public override void Unsubscribe(int index, Action<PointerInputEventArgs> listener)
		{
			if (!_pointerListeners.ContainsKey(index) || !_pointerListeners[index].Contains(listener) || _detector == null)
				return;
			List<Action<PointerInputEventArgs>> listeners = _pointerListeners[index];
			listeners.Remove(listener);

			if (listeners.Count == 0)
				_pointerListeners.Remove(index);
			if (_pointerListeners.Count == 0)
				getDetector().OnPointerInputsUpdate -= onUpdate;
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

		private void onUpdate(PointersInputEventArgs args)
		{
			for (int i = 0; i < args.PointerInputs.Count; i++)
			{
				if (!_pointerListeners.ContainsKey(i))
					continue;
				List<Action<PointerInputEventArgs>> listeners = _pointerListeners[i];
				foreach (Action<PointerInputEventArgs> listener in listeners)
					listener.Invoke(args.PointerInputs[i]);
			}
		}
	}
}
