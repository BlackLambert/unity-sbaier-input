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


		protected virtual void Update()
		{
			onPointerUpdate(_pointerInputRegistry.GetPointerInput());
		}

		private void onPointerUpdate(PointersInputEventArgs args)
		{
			string text = $"There are currently {args.Count} pointers. ";
			if (args.Count > 0)
			{
				text += $"The first pointer is at Screen Position {args.PointerInputs[0].ScreenPosition}. The pointer is over {args.PointerInputs[0].RaycastHits.Length} Objects. ";
				if (args.PointerInputs[0].IsOverUI)
					text += $"Pointer is over UI.";
			}
			Debug.Log(text);
		}
	}
}
