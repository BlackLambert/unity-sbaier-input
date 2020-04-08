using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class ButtonInputRegistryImpl : ButtonInputRegistry
	{
		private Dictionary<KeyCode, ButtonInputDetector> _detectors = new Dictionary<KeyCode, ButtonInputDetector>();
		private ButtonInputDetector _detectorPrefab;


		[Inject]
		private void Construct(ButtonInputDetector detectorPrefab)
		{
			_detectorPrefab = detectorPrefab;
		}



		public override void Subscribe(KeyCode code, ButtonState state, Action<ButtonInputEventArgs> listener)
		{
			ButtonInputDetector detector = getDetector(code);
			switch(state)
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
				throw new NotImplementedException($"The provided argument {nameof(ButtonState)} ({state}) has not been implemented yet");
			}
		}

		public override void Unsubscribe(KeyCode code, ButtonState state, Action<ButtonInputEventArgs> listener)
		{
			ButtonInputDetector detector = getDetector(code);
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
					throw new NotImplementedException($"The provided argument {nameof(ButtonState)} ({state}) has not been implemented yet");
			}
		}

		private ButtonInputDetector getDetector(KeyCode code)
		{
			ButtonInputDetector inputDetector;
			if(!_detectors.TryGetValue(code, out inputDetector))
			{
				PrefabFactory.Parameter[] parameters = new PrefabFactory.Parameter[] { new PrefabFactory.Parameter(code) };
				inputDetector = createInputDetector(_detectorPrefab, parameters);
				_detectors.Add(code, inputDetector);
			}
			return inputDetector;
		}
	}
}