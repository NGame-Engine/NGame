using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.ProjectWindow;

public interface IFileBrowserViewModelFactory
{
	FileBrowserViewModel Create();
}



public class FileBrowserViewModelFactory : IFileBrowserViewModelFactory
{
	public FileBrowserViewModel Create()
	{
		var viewModel = new FileBrowserViewModel();

		var selectedDirectoriesChangeSet = viewModel
			.DirectoryOverviewViewModel
			.SelectedDirectories
			.ToObservableChangeSet();

		selectedDirectoriesChangeSet
			.ToCollection()
			.Select(x =>
				x.Aggregate(
					"",
					(s, model) =>
						string.IsNullOrEmpty(s)
							? model.Name
							: $"{s}, {model.Name}"
				)
			)
			.BindTo(viewModel.DirectoryContentViewModel, x => x.DirectoryName);

		selectedDirectoriesChangeSet
			.TransformMany(x => x.Directories)
			.Bind(
				viewModel
					.DirectoryContentViewModel
					.Directories
			)
			.Subscribe();

		selectedDirectoriesChangeSet
			.TransformMany(x => x.Files)
			.Bind(viewModel.DirectoryContentViewModel.Files)
			.Subscribe();


		return viewModel;
	}
}