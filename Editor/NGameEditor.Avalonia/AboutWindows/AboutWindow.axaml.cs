using Avalonia.Controls;
using NGameEditor.ViewModels.AboutWindows;

namespace NGameEditor.Avalonia.AboutWindows;



public partial class AboutWindow : Window, IView<AboutWindowViewModel>
{
	public AboutWindow()
	{
		InitializeComponent();
	}
}
