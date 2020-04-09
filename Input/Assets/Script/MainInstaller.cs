using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public class MainInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind(typeof(PrefabFactory)).To<PrefabFactory>().AsTransient();
		}
	}
}