using DynamicData;
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

				viewModel
					.DirectoryOverviewViewModel
					.SelectedDirectories
					.ToObservableChangeSet()
					.Bind(
						viewModel
							.DirectoryContentViewModel
							.Directories
					)
					.Subscribe();


				return viewModel;
			}
		);
	}
}
