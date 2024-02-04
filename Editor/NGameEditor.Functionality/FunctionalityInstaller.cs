using Microsoft.Extensions.Hosting;
using NGame.Assets.Common.Setup;
using NGameEditor.Functionality.Files;
using NGameEditor.Functionality.InterProcessCommunication;
using NGameEditor.Functionality.Logging;
using NGameEditor.Functionality.Projects;
using NGameEditor.Functionality.Scenes;
using NGameEditor.Functionality.Shared;
using NGameEditor.Functionality.Users;
using NGameEditor.Functionality.Windows;

namespace NGameEditor.Functionality;



public static class FunctionalityInstaller
{
	public static void AddFunctionality(this IHostApplicationBuilder builder)
	{
		builder.AddWindows();


		builder.AddLogging();

		builder.AddFrontendIpc();

		builder.AddNGameCommon();
		builder.AddNGameEditorDomainShared();
		builder.AddConfigurations();
		builder.AddFiles();
		builder.AddScenes();
		builder.AddProjects();
	}
}
