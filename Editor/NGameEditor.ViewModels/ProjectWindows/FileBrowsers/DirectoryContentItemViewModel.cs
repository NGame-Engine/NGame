using System.Windows.Input;

namespace NGameEditor.ViewModels.ProjectWindows.FileBrowsers;



public class DirectoryContentItemViewModel : ViewModelBase
{
	public string Name { get; set; } = "";
	public string DisplayName => Path.GetFileName(Name);
	public bool IsFolder { get; set; }
	public string Icon { get; set; } = "❔";
	public ICommand? Open { get; set; }
}
