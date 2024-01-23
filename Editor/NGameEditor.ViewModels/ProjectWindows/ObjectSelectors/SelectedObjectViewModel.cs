namespace NGameEditor.ViewModels.ProjectWindows.ObjectSelectors;



public class SelectedObjectViewModel : ViewModelBase
{
	private string _name = "";

	public string Name
	{
		get => _name;
		set => this.RaiseAndSetIfChanged(ref _name, value);
	}

	private string _kindName = "";

	public string KindName
	{
		get => _kindName;
		set => this.RaiseAndSetIfChanged(ref _kindName, value);
	}

	private string _path = "";

	public string Path
	{
		get => _path;
		set => this.RaiseAndSetIfChanged(ref _path, value);
	}
}
