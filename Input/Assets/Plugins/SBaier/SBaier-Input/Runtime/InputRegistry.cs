
using UnityEngine;
using Zenject;

namespace SBaier.Input
{
	public abstract class InputRegistry 
	{
		private PrefabFactory _prefabFactory;
		private GameObject _hook;

		[Inject]
		private void Construct(PrefabFactory prefabFactory,
			GameObject hook)
		{
			_prefabFactory = prefabFactory;
			_hook = hook;
		}

		protected TInputDetector createInputDetector<TInputDetector>(TInputDetector prefab,
			PrefabFactory.Parameter[] parameters) where TInputDetector: InputDetector
		{
			TInputDetector inputDetector = _prefabFactory.Create(prefab, parameters);
			inputDetector.transform.SetParent(_hook.transform, false);
			return inputDetector;
		}
	}
}