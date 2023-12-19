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
	}
}
