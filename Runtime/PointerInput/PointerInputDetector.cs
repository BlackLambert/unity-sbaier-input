using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using System.Linq;

namespace SBaier.Input
{
	public abstract class PointersInputDetector : InputDetector
	{
		private float _maxRaycastDistance;
		private Camera _inputCamera;
		private bool _triggerRaycastable;


		[Inject]
		private void Construct(float maxRaycastDistance, [Inject(Id = "InputCamera")]Camera inputCamera,
			bool triggerRaycastable)
		{
			_maxRaycastDistance = maxRaycastDistance;
			_inputCamera = inputCamera;
			_triggerRaycastable = triggerRaycastable;
		}


		public abstract PointersInputEventArgs GetPointerInputs();

		protected RaycastAllResult raycastAt(Vector2 screenPosition)
		{
			if (_inputCamera == null)
				return new RaycastAllResult(new PointerRaycastHit[0], false);
			Ray ray = _inputCamera.ScreenPointToRay(screenPosition);
			List<PointerRaycastHit> objectHits = new List<PointerRaycastHit>();
			objectHits.AddRange(raycastPhysics(ray));
			objectHits.AddRange(raycast2D(ray));
			List<PointerRaycastHit> uIHits = raycastUI(screenPosition);
			bool overUI = uIHits.Count > 0;
			uIHits.AddRange(objectHits.OrderBy(hit => hit.Distance));
			return new RaycastAllResult(uIHits.ToArray(), overUI);
		}

		private List<PointerRaycastHit> raycastPhysics(Ray ray)
		{
			List<PointerRaycastHit> result = new List<PointerRaycastHit>();
			RaycastHit[] hits = Physics.RaycastAll(ray, _maxRaycastDistance);
			foreach (RaycastHit hit in hits)
			{
				if (!_triggerRaycastable && hit.collider.isTrigger)
					continue;
				result.Add(new PointerRaycastHit(hit.transform, hit.point, hit.distance));
			}
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