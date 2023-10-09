using System.Numerics;

namespace NGame.Services;



public class TappedEventArgs : EventArgs
{
	public TappedEventArgs(Vector2 relativePosition)
	{
		RelativePosition = relativePosition;
	}


	public Vector2 RelativePosition { get; }
}
