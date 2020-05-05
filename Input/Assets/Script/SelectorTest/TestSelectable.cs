

using UnityEngine;

namespace SBaier.Input.Test
{
	public class TestSelectable : Selectable
	{
		[SerializeField]
		private bool _deselectOnDoubleSelect = true;
		public override bool DeselectOnDoubleSelect => _deselectOnDoubleSelect;

		[SerializeField]
		private bool _selectAgainOnDoubleSelect = true;
		public override bool SelectAgainOnDoubleSelect => _selectAgainOnDoubleSelect;
	}
}