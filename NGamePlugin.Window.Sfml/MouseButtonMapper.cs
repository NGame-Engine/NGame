using NGame.OsWindows;
using SFML.Window;

namespace NGamePlugin.Window.Sfml;

public static class MouseButtonMapper
{
	public static MouseButton Map(Mouse.Button button) =>
		button switch
		{
			Mouse.Button.Left => MouseButton.Left,
			Mouse.Button.Right => MouseButton.Right,
			Mouse.Button.Middle => MouseButton.Middle,
			Mouse.Button.XButton1 => MouseButton.XButton1,
			Mouse.Button.XButton2 => MouseButton.XButton2,
			_ => MouseButton.Unknown
		};
}
