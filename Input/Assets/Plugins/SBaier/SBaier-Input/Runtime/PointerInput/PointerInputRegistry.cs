using System;

namespace SBaier.Input
{
	public abstract class PointerInputRegistry : InputRegistry
	{
		public abstract PointersInputEventArgs GetPointerInput();
		public abstract PointerInputEventArgs GetPointerInput(int index);
	}
}