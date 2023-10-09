namespace NGame.Services;



public enum SwipeDirection
{
	Unknown,
	Right,
	Left,
	Up,
	Down
}



public class SwipedEventArgs : EventArgs
{
	public SwipedEventArgs(SwipeDirection direction)
	{
		Direction = direction;
	}


	public SwipeDirection Direction { get; }
}
