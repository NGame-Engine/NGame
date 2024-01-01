using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using Microsoft.Extensions.Logging;
using NGame.Assets;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.ProjectWindow;



public interface IFileBrowserViewModelFactory
{
	FileBrowserViewModel Create();
}



public class FileBrowserViewModelFactory(
	IClientRunner<IBackendApi> clientRunner,
	ILogger<FileBrowserViewModelFactory> logger
) : IFileBrowserViewModelFactory
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
			.Transform(x => Map(x, viewModel))
			.Bind(viewModel.DirectoryContentViewModel.Items)
			.Subscribe();

		selectedDirectoriesChangeSet
			.TransformMany(x => x.Files)
			.Transform(Map)
			.Bind(viewModel.DirectoryContentViewModel.Items)
			.Subscribe();


		return viewModel;
	}


	private static DirectoryContentItemViewModel Map(
		DirectoryViewModel directoryViewModel,
		FileBrowserViewModel viewModel
	) =>
		new(directoryViewModel.Name)
		{
			Icon = "📁",
			Open = ReactiveCommand.Create(() =>
			{
				var selectedDirectories = viewModel
					.DirectoryOverviewViewModel
					.SelectedDirectories;

				selectedDirectories.Clear();
				selectedDirectories.Add(directoryViewModel);
			})
		};


	private DirectoryContentItemViewModel Map(FileViewModel fileViewModel) =>
		new(fileViewModel.Name)
		{
			Icon =
				fileViewModel.Name.EndsWith(AssetConventions.SceneFileEnding)
					? "🏞️"
					: "❔",
			Open = ReactiveCommand.Create(() =>
				clientRunner
					.GetClientService()
					.Then(x => x.OpenFile(new AbsolutePath(fileViewModel.Name)))
					.IfError(logger.Log)
			)
		};
}
