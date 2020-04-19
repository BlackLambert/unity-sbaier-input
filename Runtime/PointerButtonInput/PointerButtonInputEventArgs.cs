
using UnityEngine;

namespace SBaier.Input
{
	public class PointerButtonInputEventArgs 
	{
		public int Index { get; }
		public ButtonState State { get; }
		/// <summary>
		/// The duration in seconds this Button/Finger/Key is within its pressed state.
		/// </summary>
		public float Duration { get; }

		public PointerButtonInputEventArgs(int index,
			ButtonState state,
			float duration)
		{
			Index = index;
			State = state;
			Duration = duration;
		}
	}
}