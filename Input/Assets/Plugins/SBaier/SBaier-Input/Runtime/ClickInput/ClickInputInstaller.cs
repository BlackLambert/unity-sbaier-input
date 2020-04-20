using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class ClickInputInstaller : MonoInstaller
	{
		private const string _inputDetectorPrefabPath = "Prefabs/ClickInputDetector";

		[SerializeField]
		private float _maxClickDuration = 0.33f;
		[SerializeField]
		private float _maxPointerDelta = 10;

		public override void InstallBindings()
		{
			GameObject hook = new GameObject("ClickInputDetectors");
			Container.Bind(typeof(ClickInputRegistry)).To<ClickInputRegistryImpl>().AsSingle().WithArguments(hook);
			Container.Bind(typeof(ClickInputDetector)).To<BasicClickInputDetector>().FromResource(_inputDetectorPrefabPath).AsTransient();
			Container.Bind(typeof(ClickInputParameter)).To<ClickInputParameter>().FromInstance(new ClickInputParameter(_maxClickDuration, _maxPointerDelta)).AsTransient();
		}
	}
}