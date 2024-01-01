using System.Windows.Input;

namespace NGameEditor.ViewModels.ProjectWindows.FileBrowsers;



public class DirectoryContentItemViewModel(
	string name
) : ViewModelBase
{
	public string Name { get; } = name;
	public string DisplayName => Path.GetFileName(Name);
	public string Icon { get; set; } = "❔";
	public ICommand? Open { get; set; }
}
