using System.Numerics;

namespace NGame.Services;



public class PanEventArgs : EventArgs
{
	public PanEventArgs(int gestureId, GestureStatus statusType, Vector2 totalChangeSinceBeginning)
	{
		GestureId = gestureId;
		StatusType = statusType;
		TotalChangeSinceBeginning = totalChangeSinceBeginning;
	}


	/// <summary>Gets the identifier for the gesture that raised the event.</summary>
	public int GestureId { get; }

	public GestureStatus StatusType { get; }
	public Vector2 TotalChangeSinceBeginning { get; }
}
