
using UnityEngine;

namespace SBaier.Input
{
	public class ButtonInputEventArgs 
	{
		public KeyCode Key { get; }
		public ButtonState State { get; }
		/// <summary>
		/// The duration in seconds this key is within its pressed state.
		/// </summary>
		public float Duration { get; }

		public ButtonInputEventArgs(KeyCode key,
			ButtonState state,
			float duration)
		{
			Key = key;
			State = state;
			Duration = duration;
		}
	}
}