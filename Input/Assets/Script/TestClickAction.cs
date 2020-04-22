using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Input.Test
{
	public class TestClickAction : MonoBehaviour
	{
		[SerializeField]
		private Clickable _clickable = null;
		[SerializeField]
		private MeshRenderer _meshRenderer = null;

		protected virtual void Start()
		{
			_clickable.OnClick += onClick;
		}

		protected virtual void OnDestroy()
		{
			_clickable.OnClick -= onClick;
		}

		private void onClick(ClickableInputEventArgs args)
		{
			Color col = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			_meshRenderer.material.color = col;
			Debug.Log($"Changing color to {col} | Clicked at {args.RaycastHit.Point}");
		}
	}
}