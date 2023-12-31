using System.Windows.Input;

namespace NGameEditor.ViewModels.LauncherWindows.HistoryViews;



public class HistoryEntryViewModel(
	string path,
	DateTime lastUsed,
	ICommand openProject
) : ViewModelBase
{
	public string FilePath => path;
	public string Name => Path.GetFileName(FilePath);
	public string LastUsed => lastUsed.ToString("u");


	public void OpenProject()
	{
		openProject.Execute(null);
	}
}
