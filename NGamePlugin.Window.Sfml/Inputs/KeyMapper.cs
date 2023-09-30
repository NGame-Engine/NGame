﻿using NGame.Inputs;
using SFML.Window;

namespace NGamePlugin.Window.Sfml.Inputs;

public static class KeyMapper
{
	public static KeyCode Map(Keyboard.Key key) =>
		key switch
		{
			Keyboard.Key.A => KeyCode.A,
			Keyboard.Key.B => KeyCode.B,
			Keyboard.Key.C => KeyCode.C,
			Keyboard.Key.D => KeyCode.D,
			Keyboard.Key.E => KeyCode.E,
			Keyboard.Key.F => KeyCode.F,
			Keyboard.Key.G => KeyCode.G,
			Keyboard.Key.H => KeyCode.H,
			Keyboard.Key.I => KeyCode.I,
			Keyboard.Key.J => KeyCode.J,
			Keyboard.Key.K => KeyCode.K,
			Keyboard.Key.L => KeyCode.L,
			Keyboard.Key.M => KeyCode.M,
			Keyboard.Key.N => KeyCode.N,
			Keyboard.Key.O => KeyCode.O,
			Keyboard.Key.P => KeyCode.P,
			Keyboard.Key.Q => KeyCode.Q,
			Keyboard.Key.R => KeyCode.R,
			Keyboard.Key.S => KeyCode.S,
			Keyboard.Key.T => KeyCode.T,
			Keyboard.Key.U => KeyCode.U,
			Keyboard.Key.V => KeyCode.V,
			Keyboard.Key.W => KeyCode.W,
			Keyboard.Key.X => KeyCode.X,
			Keyboard.Key.Y => KeyCode.Y,
			Keyboard.Key.Z => KeyCode.Z,
			Keyboard.Key.Num0 => KeyCode.Num0,
			Keyboard.Key.Num1 => KeyCode.Num1,
			Keyboard.Key.Num2 => KeyCode.Num2,
			Keyboard.Key.Num3 => KeyCode.Num3,
			Keyboard.Key.Num4 => KeyCode.Num4,
			Keyboard.Key.Num5 => KeyCode.Num5,
			Keyboard.Key.Num6 => KeyCode.Num6,
			Keyboard.Key.Num7 => KeyCode.Num7,
			Keyboard.Key.Num8 => KeyCode.Num8,
			Keyboard.Key.Num9 => KeyCode.Num9,
			Keyboard.Key.Escape => KeyCode.Escape,
			Keyboard.Key.LControl => KeyCode.LControl,
			Keyboard.Key.LShift => KeyCode.LShift,
			Keyboard.Key.LAlt => KeyCode.LAlt,
			Keyboard.Key.LSystem => KeyCode.LSystem,
			Keyboard.Key.RControl => KeyCode.RControl,
			Keyboard.Key.RShift => KeyCode.RShift,
			Keyboard.Key.RAlt => KeyCode.RAlt,
			Keyboard.Key.RSystem => KeyCode.RSystem,
			Keyboard.Key.Menu => KeyCode.Menu,
			Keyboard.Key.LBracket => KeyCode.LBracket,
			Keyboard.Key.RBracket => KeyCode.RBracket,
			Keyboard.Key.Semicolon => KeyCode.Semicolon,
			Keyboard.Key.Comma => KeyCode.Comma,
			Keyboard.Key.Period => KeyCode.Period,
			Keyboard.Key.Quote => KeyCode.Quote,
			Keyboard.Key.Slash => KeyCode.Slash,
			Keyboard.Key.Backslash => KeyCode.Backslash,
			Keyboard.Key.Tilde => KeyCode.Tilde,
			Keyboard.Key.Equal => KeyCode.Equal,
			Keyboard.Key.Hyphen => KeyCode.Hyphen,
			Keyboard.Key.Space => KeyCode.Space,
			Keyboard.Key.Enter => KeyCode.Enter,
			Keyboard.Key.Backspace => KeyCode.Backspace,
			Keyboard.Key.Tab => KeyCode.Tab,
			Keyboard.Key.PageUp => KeyCode.PageUp,
			Keyboard.Key.PageDown => KeyCode.PageDown,
			Keyboard.Key.End => KeyCode.End,
			Keyboard.Key.Home => KeyCode.Home,
			Keyboard.Key.Insert => KeyCode.Insert,
			Keyboard.Key.Delete => KeyCode.Delete,
			Keyboard.Key.Add => KeyCode.Add,
			Keyboard.Key.Subtract => KeyCode.Subtract,
			Keyboard.Key.Multiply => KeyCode.Multiply,
			Keyboard.Key.Divide => KeyCode.Divide,
			Keyboard.Key.Left => KeyCode.Left,
			Keyboard.Key.Right => KeyCode.Right,
			Keyboard.Key.Up => KeyCode.Up,
			Keyboard.Key.Down => KeyCode.Down,
			Keyboard.Key.Numpad0 => KeyCode.Numpad0,
			Keyboard.Key.Numpad1 => KeyCode.Numpad1,
			Keyboard.Key.Numpad2 => KeyCode.Numpad2,
			Keyboard.Key.Numpad3 => KeyCode.Numpad3,
			Keyboard.Key.Numpad4 => KeyCode.Numpad4,
			Keyboard.Key.Numpad5 => KeyCode.Numpad5,
			Keyboard.Key.Numpad6 => KeyCode.Numpad6,
			Keyboard.Key.Numpad7 => KeyCode.Numpad7,
			Keyboard.Key.Numpad8 => KeyCode.Numpad8,
			Keyboard.Key.Numpad9 => KeyCode.Numpad9,
			Keyboard.Key.F1 => KeyCode.F1,
			Keyboard.Key.F2 => KeyCode.F2,
			Keyboard.Key.F3 => KeyCode.F3,
			Keyboard.Key.F4 => KeyCode.F4,
			Keyboard.Key.F5 => KeyCode.F5,
			Keyboard.Key.F6 => KeyCode.F6,
			Keyboard.Key.F7 => KeyCode.F7,
			Keyboard.Key.F8 => KeyCode.F8,
			Keyboard.Key.F9 => KeyCode.F9,
			Keyboard.Key.F10 => KeyCode.F10,
			Keyboard.Key.F11 => KeyCode.F11,
			Keyboard.Key.F12 => KeyCode.F12,
			Keyboard.Key.F13 => KeyCode.F13,
			Keyboard.Key.F14 => KeyCode.F14,
			Keyboard.Key.F15 => KeyCode.F15,
			Keyboard.Key.Pause => KeyCode.Pause,
			_ => KeyCode.Unknown
		};
}