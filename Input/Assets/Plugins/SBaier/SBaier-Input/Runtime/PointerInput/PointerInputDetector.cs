using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace SBaier.Input
{
	public abstract class PointersInputDetector : InputDetector
	{
		private float _maxRaycastDistance;
		private Camera _inputCamera;


		[Inject]
		private void Construct(float maxRaycastDistance, [Inject(Id = "InputCamera")]Camera inputCamera)
		{
			_maxRaycastDistance = maxRaycastDistance;
			_inputCamera = inputCamera;
		}


		public abstract PointersInputEventArgs GetPointerInputs();

		protected RaycastAllResult raycastAt(Vector2 screenPosition)
		{
			List<PointerRaycastHit> hits = new List<PointerRaycastHit>();
			if (_inputCamera == null)
				return new RaycastAllResult(new PointerRaycastHit[0], false);
			Ray ray = _inputCamera.ScreenPointToRay(screenPosition);
			hits.AddRange(raycastPhysics(ray));
			hits.AddRange(raycast2D(ray));
			List<PointerRaycastHit> uIHits = raycastUI(screenPosition);
			hits.AddRange(uIHits);
			bool overUI = uIHits.Count > 0;
			return new RaycastAllResult(hits.ToArray(), overUI);
		}

		private List<PointerRaycastHit> raycastPhysics(Ray ray)
		{
			List<PointerRaycastHit> result = new List<PointerRaycastHit>();
			RaycastHit[] hits = Physics.RaycastAll(ray, _maxRaycastDistance);
			foreach (RaycastHit hit in hits)
				result.Add(new PointerRaycastHit(hit.transform, hit.point, hit.distance));
			return result;
		}

		private List<PointerRaycastHit> raycast2D(Ray ray)
		{
			List<PointerRaycastHit> result = new List<PointerRaycastHit>();
			RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, _maxRaycastDistance);
			foreach (RaycastHit2D hit in hits)
				result.Add(new PointerRaycastHit(hit.transform, hit.point, hit.distance));
			return result;
		}

		private List<PointerRaycastHit> raycastUI(Vector2 screenPosition)
		{
			List<PointerRaycastHit> result = new List<PointerRaycastHit>();
			EventSystem system = EventSystem.current;
			if (system == null)
				return result;
			List<RaycastResult> hits = new List<RaycastResult>();
			PointerEventData pointerEvent = new PointerEventData(system);
			pointerEvent.position = screenPosition;
			system.RaycastAll(pointerEvent, hits);
			foreach(RaycastResult hit in hits)
				result.Add(new PointerRaycastHit(hit.gameObject.transform, hit.worldPosition, hit.distance));
			return result;
		}

		protected struct RaycastAllResult
		{
			public PointerRaycastHit[] Hits { get; }
			public bool IsOverUI { get; }


			public RaycastAllResult(PointerRaycastHit[] hits,
				bool isOverUI)
			{
				Hits = hits;
				IsOverUI = isOverUI;
			}
		}
	}
}