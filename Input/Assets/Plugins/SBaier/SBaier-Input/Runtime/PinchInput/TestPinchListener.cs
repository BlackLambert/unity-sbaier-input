using UnityEngine;
using Zenject;

namespace SBaier.Input.Test
{
	public class TestPinchListener : MonoBehaviour
	{
		private PinchInputRegistry _registry;

		[Inject]
		private void Construct(PinchInputRegistry registry)
		{
			_registry = registry;
		}

		protected virtual void Start()
		{
			_registry.Subscribe(PinchState.Ongoing, onPinch);
		}

		private void onPinch(PinchInputEventArgs args)
		{
			string log = $"The pinch is active! The delta is {args.Delta} along the {args.Axis} axis. Pinching for {args.Duration} seconds";
			Debug.Log(log);
		}

		protected virtual void OnDestroy()
		{
			_registry.Unsubscribe(PinchState.Ongoing, onPinch);
		}
	}
}

