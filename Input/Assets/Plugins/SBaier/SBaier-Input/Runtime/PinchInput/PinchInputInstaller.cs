using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class PinchInputInstaller : MonoInstaller
	{
		private const string _inputDetectorPrefabPath = "Prefabs/PinchInputDetector";


		public override void InstallBindings()
		{
			GameObject hook = new GameObject("PinchInputDetectors");
			Container.Bind(typeof(PinchInputRegistry)).To<PinchInputRegistryImpl>().AsSingle().WithArguments(hook);
			Container.Bind(typeof(PinchInputDetector)).To<BasicPinchInputDetector>().FromResource(_inputDetectorPrefabPath).AsTransient();
		}
	}
}