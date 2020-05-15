using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class PointerInputInstaller : MonoInstaller
	{
		private const string _mouseInputDetectorPrefabPath = "Prefabs/MousePointerInputDetector";
		private const string _touchInputDetectorPrefabPath = "Prefabs/TouchPointerInputDetector";

		[SerializeField]
		private int _maxPointerCount = 2;
		[SerializeField]
		private float _maximalRaycastDistance = 200f;
		[SerializeField]
		private Camera _inputCamera = null;
		[SerializeField]
		private bool _triggerRaycastable = false;

		public override void InstallBindings()
		{
			GameObject hook = new GameObject("PointerInputDetector");
			Container.Bind(typeof(PointerInputRegistry)).To<PointerInputRegistryImpl>().AsSingle().WithArguments(hook, _maxPointerCount, _maximalRaycastDistance, _triggerRaycastable);
			if(Application.isMobilePlatform)
				Container.Bind(typeof(PointersInputDetector)).To<TouchPointersInputDetector>().FromResource(_touchInputDetectorPrefabPath).AsTransient();
			else
				Container.Bind(typeof(PointersInputDetector)).To<MousePointerInputDetector>().FromResource(_mouseInputDetectorPrefabPath).AsTransient();
			Container.Bind(typeof(Camera)).WithId("InputCamera").To<Camera>().FromInstance(_inputCamera).AsSingle();
		}
	}
}