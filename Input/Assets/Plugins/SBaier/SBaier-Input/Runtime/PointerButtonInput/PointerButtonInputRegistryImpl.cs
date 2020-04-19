using System;
using System.Collections.Generic;
using Zenject;

namespace SBaier.Input
{
	public class PointerButtonInputRegistryImpl : PointerButtonInputRegistry
	{
		private Dictionary<int, PointerButtonInputDetector> _detectors = new Dictionary<int, PointerButtonInputDetector>();
		private PointerButtonInputDetector _detectorPrefab;

		[Inject]
		private void Construct(PointerButtonInputDetector detectorPrefab)
		{
			_detectorPrefab = detectorPrefab;
		}




		public override void Subscribe(int index, ButtonState state, Action<PointerButtonInputEventArgs> listener)
		{
			PointerButtonInputDetector detector = getDetector(index);
			switch (state)
			{
				case ButtonState.Down:
					detector.OnDown += listener;
					break;
				case ButtonState.Pressed:
					detector.OnPress += listener;
					break;
				case ButtonState.Released:
					detector.OnReleased += listener;
					break;
				case ButtonState.None:
					throw new ArgumentException($"The provided argument {nameof(ButtonState.None)} is not valid");
				default:
					throw new NotImplementedException($"The provided argument {nameof(ButtonState)} ({state}) has not been implemented yet");
			}
		}

		public override void Unsubscribe(int index, ButtonState state, Action<PointerButtonInputEventArgs> listener)
		{
			PointerButtonInputDetector detector = getDetector(index);
			switch (state)
			{
				case ButtonState.Down:
					detector.OnDown -= listener;
					break;
				case ButtonState.Pressed:
					detector.OnPress -= listener;
					break;
				case ButtonState.Released:
					detector.OnReleased -= listener;
					break;
				case ButtonState.None:
					throw new ArgumentException($"The provided argument {nameof(ButtonState.None)} is not valid");
				default:
					throw new NotImplementedException($"The provided argument {nameof(ButtonState)} ({state}) has not been implemented yet");
			}
		}

		private PointerButtonInputDetector getDetector(int index)
		{
			PointerButtonInputDetector inputDetector;
			if(!_detectors.TryGetValue(index, out inputDetector))
			{
				PrefabFactory.Parameter[] parameters = new PrefabFactory.Parameter[] { new PrefabFactory.Parameter(index) };
				inputDetector = createInputDetector(_detectorPrefab, parameters);
				_detectors.Add(index, inputDetector);
			}
			return inputDetector;
		}
	}
}