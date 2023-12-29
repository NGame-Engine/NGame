using System.Reactive.Linq;
using DynamicData;
using DynamicData.Alias;
using DynamicData.Binding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Setup;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Setup;
using NGameEditor.Functionality.Controllers;
using NGameEditor.Functionality.Logging;
using NGameEditor.Functionality.Projects;
using NGameEditor.Functionality.Scenes;
using NGameEditor.Functionality.Shared;
using NGameEditor.Functionality.Users;
using NGameEditor.Functionality.Windows;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;
using ReactiveUI;

namespace NGameEditor.Functionality;



public static class FunctionalityInstaller
{
	public static void AddFunctionality(this IHostApplicationBuilder builder)
	{
		builder.AddWindows();
		builder.AddControllers();

		builder.AddLogging();
		builder.AddBridge();

		builder.Services.AddNGameCommon();
		builder.Services.AddNGameEditorDomainShared();
		builder.Services.AddBackend();
		builder.Services.AddConfigurations();
		builder.Services.AddScenes();
		builder.Services.AddProjects();

		builder.AddFileBrowserViewModel();
	}


	public static void AddFileBrowserViewModel(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<FileBrowserViewModel>(_ =>
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
		);
	}
}
