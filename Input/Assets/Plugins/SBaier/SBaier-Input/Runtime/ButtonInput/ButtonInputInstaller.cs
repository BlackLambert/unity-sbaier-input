using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class ButtonInputInstaller : MonoInstaller
	{
		private const string _inputDetectorPrefabPath = "Prefabs/ButtonInputDetector";

		public override void InstallBindings()
		{
			GameObject hook = new GameObject("ButtonInputDetectors");
			Container.Bind(typeof(ButtonInputRegistry)).To<ButtonInputRegistryImpl>().AsSingle().WithArguments(hook);
			Container.Bind(typeof(ButtonInputDetector)).To<BasicButtonInputDetector>().FromResource(_inputDetectorPrefabPath).AsTransient();
			Container.Bind(typeof(PrefabFactory)).To<PrefabFactory>().AsTransient();
		}
	}
}