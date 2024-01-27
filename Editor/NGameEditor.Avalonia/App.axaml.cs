using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NGameEditor.Functionality.Windows;
using Splat;

namespace NGameEditor.Avalonia;



public class App : Application
{
	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}


	public override void OnFrameworkInitializationCompleted()
	{
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
		{
			var launcherWindowOpener = Locator.Current.GetService<ILauncherWindowOpener>()!;
			launcherWindowOpener.Open();
		}

		base.OnFrameworkInitializationCompleted();
	}
}
