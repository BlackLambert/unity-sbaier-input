

namespace SBaier.Input.Test
{
	public class TestSelectable : Selectable
	{
		public override bool DeselectOnDoubleSelect => false;

		public override bool SelectAgainOnDoubleSelect => true;
	}
}