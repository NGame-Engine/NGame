using NGame.Inputs;
using SFML.Window;

namespace NGamePlugin.Window.Sfml.Inputs;

public static class JoystickAxisMapper
{
	public static JoystickAxis Map(Joystick.Axis axis) =>
		axis switch
		{
			Joystick.Axis.X => JoystickAxis.X,
			Joystick.Axis.Y => JoystickAxis.Y,
			Joystick.Axis.Z => JoystickAxis.Z,
			Joystick.Axis.R => JoystickAxis.R,
			Joystick.Axis.U => JoystickAxis.U,
			Joystick.Axis.V => JoystickAxis.V,
			Joystick.Axis.PovX => JoystickAxis.PovX,
			Joystick.Axis.PovY => JoystickAxis.PovY,
			_ => JoystickAxis.Unknown
		};
}
