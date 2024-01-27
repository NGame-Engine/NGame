using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using Microsoft.Extensions.Logging;
using NGame.Assets;
using NGame.SceneAssets;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.ProjectWindow;



public interface IDirectoryContentViewModelFactory
{
	DirectoryContentViewModel Create();
}



public class DirectoryContentViewModelFactory(
	IClientRunner<IBackendApi> clientRunner,
	ILogger<DirectoryContentViewModelFactory> logger,
	DirectoryOverviewViewModel directoryOverviewViewModel
) : IDirectoryContentViewModelFactory
{
	public DirectoryContentViewModel Create()
	{
		var viewModel = new DirectoryContentViewModel();

		var selectedDirectoriesChangeSet =
			directoryOverviewViewModel
				.SelectedDirectories
				.ToObservableChangeSet();

		selectedDirectoriesChangeSet
			.ToCollection()
			.Select(x =>
				x.Aggregate<DirectoryViewModel, string>("",
					(s, model) =>
						string.IsNullOrEmpty(s)
							? model.Name
							: $"{s}, {model.Name}"
				)
			)
			.BindTo(viewModel, x => x.DirectoryName);


		selectedDirectoriesChangeSet
			.TransformMany(x => x.Directories)
			.Transform(Map)
			.Bind(viewModel.Items)
			.Subscribe();

		selectedDirectoriesChangeSet
			.TransformMany(x => x.Files)
			.Transform(Map)
			.Bind(viewModel.Items)
			.Subscribe();


		return viewModel;
	}


	private DirectoryContentItemViewModel Map(DirectoryViewModel directoryViewModel) =>
		new()
		{
			Name = directoryViewModel.Name,
			IsFolder = true,
			Icon = "📁",
			Open = ReactiveCommand.Create(() =>
			{
				var selectedDirectories = directoryOverviewViewModel.SelectedDirectories;

				selectedDirectories.Clear();
				selectedDirectories.Add(directoryViewModel);
			})
		};


	private DirectoryContentItemViewModel Map(FileViewModel fileViewModel) =>
		new()
		{
			Name = fileViewModel.Name,
			IsFolder = false,
			Icon =
				fileViewModel.AssetTypeIdentifier == AssetAttribute.GetDiscriminator(typeof(SceneAsset))
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
