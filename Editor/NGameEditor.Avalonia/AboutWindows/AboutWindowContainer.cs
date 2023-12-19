using System;
using NGameEditor.Functionality.Windows;

namespace NGameEditor.Avalonia.AboutWindows;



public class AboutWindowContainer(Func<AboutWindow> factoryMethod) : IAboutWindow
{
	private AboutWindow? AboutWindow { get; set; }


	public void Open()
	{
		if (AboutWindow != null) throw new InvalidOperationException();

		AboutWindow = factoryMethod();
		AboutWindow.Show();
	}


	public void Close()
	{
		if (AboutWindow == null) throw new InvalidOperationException();

		AboutWindow.Close();
		AboutWindow = null;
	}
}
