using System;
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class PointerButtonInputInstaller : MonoInstaller
	{
		private const string _touchInputDetectorPrefabPath = "Prefabs/TouchButtonInputDetector";
		private const string _mouseInputDetectorPrefabPath = "Prefabs/MouseButtonInputDetector";

		public override void InstallBindings()
		{
			GameObject hook = new GameObject("PointerButtonInputDetectors");
			Container.Bind(typeof(PointerButtonInputRegistry)).To<PointerButtonInputRegistryImpl>().AsSingle().WithArguments(hook);
			if (Application.isMobilePlatform)
				Container.Bind(typeof(PointerButtonInputDetector)).To<TouchPointersInputDetector>().FromResource(_touchInputDetectorPrefabPath).AsTransient();
			else if (Application.isConsolePlatform)
				throw new NotImplementedException($"The Platform's {Application.platform} Pointer Button inputs are not supported yet");
			else
				Container.Bind(typeof(PointerButtonInputDetector)).To<MouseButtonInputDetector>().FromResource(_mouseInputDetectorPrefabPath).AsTransient();
		}
	}
}