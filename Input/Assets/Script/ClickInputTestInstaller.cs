using UnityEngine;
using Zenject;

namespace SBaier.Input.Test
{
	public class ClickInputTestInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind(typeof(TestSelector)).To<TestSelector>().FromNewComponentOnNewGameObject().AsSingle().WithArguments(true).NonLazy();
		}
	}
}