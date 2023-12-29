using System;
using NGameEditor.Functionality.Windows;

namespace NGameEditor.Avalonia.LauncherWindows;



public class LauncherWindowContainer(Func<LauncherWindow> factoryMethod) : ILauncherWindow
{
	private LauncherWindow? LauncherWindow { get; set; }


	public void Open()
	{
		if (LauncherWindow != null) throw new InvalidOperationException();

		LauncherWindow = factoryMethod();
		LauncherWindow.Show();
	}


	public void Close()
	{
		if (LauncherWindow == null) throw new InvalidOperationException();

		LauncherWindow.Close();
		LauncherWindow = null;
	}
}
