using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SBaier.Input.Test
{

	public class TestPointerListener : MonoBehaviour
	{
		private PointerInputRegistry _pointerInputRegistry;


		[Inject]
		private void Construct(PointerInputRegistry pointerInputRegistry)
		{
			_pointerInputRegistry = pointerInputRegistry;
		}


		protected virtual void Start()
		{
			_pointerInputRegistry.Subscribe(onPointerUpdate);
		}

		protected virtual void OnDestroy()
		{
			_pointerInputRegistry.Unsubscribe(onPointerUpdate);
		}

		private void onPointerUpdate(PointersInputEventArgs args)
		{
			Debug.Log($"There are currently {args.Count} pointers.");
			if(args.Count > 0)
				Debug.Log($"The first pointer is at Screen Position {args.PointerInputs[0].ScreenPosition}.");
		}
	}
}
