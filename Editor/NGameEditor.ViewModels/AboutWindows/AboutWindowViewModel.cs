namespace NGameEditor.ViewModels.AboutWindows;



public class AboutWindowViewModel : ViewModelBase
{
	public string Version => $"{GetType().Assembly.GetName().Version!}";
}
