using System.Numerics;

namespace NGame.Services;



public class PinchEventArgs : EventArgs
{
	public PinchEventArgs(GestureStatus status, float scale, Vector2 scaleOrigin)
	{
		Status = status;
		Scale = scale;
		ScaleOrigin = scaleOrigin;
	}


	/// <summary>Whether the gesture started, is running, or has finished.</summary>
	/// <value>Whether the gesture started, is running, or has finished.</value>
	/// <remarks>
	///   <para>The origin of the pinch, <see cref="P:Microsoft.Maui.Controls.PinchGestureUpdatedEventArgs.ScaleOrigin" />, is the center of the pinch gesture, and changes if the user translates their pinch while they scale. Application developers may want to store the pinch origin when the gesture begins and use it for all scaling operations for that gesture.</para>
	///   <para>The initial value of the <see cref="P:Microsoft.Maui.Controls.PinchGestureUpdatedEventArgs.Scale" /> property for each new pinch gesture is <c>1.0</c>.</para>
	/// </remarks>
	public GestureStatus Status { get; }

	/// <summary>The relative size of the user's pinch gesture since the last update was received.</summary>
	/// <value>The distance between the user's digits, divided by the last reported distance between the user's digits in the pinch gesture.</value>
	/// <remarks>
	///   <para>The initial value for each new pinch gesture is <c>1.0</c>.</para>
	/// </remarks>
	public float Scale { get; }

	/// <summary>The updated origin of the pinch gesture.</summary>
	/// <value>The midpoint of the pinch gesture.</value>
	/// <remarks>
	///   <para>The origin of the pinch is the center of the pinch gesture, and changes if the user translates their pinch while they scale. Application developers may want to store the pinch origin when the gesture begins and use it for all scaling operations for that gesture.</para>
	/// </remarks>
	public Vector2 ScaleOrigin { get; }
}
