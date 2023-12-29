using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.ViewModels.Controllers;
using NGameEditor.ViewModels.LauncherWindows;

namespace NGameEditor.Functionality.Controllers;



public static class ControllersInstaller
{
	public static void AddControllers(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<IProjectController, ProjectController>();
		builder.Services.AddTransient<IMenuController, MenuController>();

		builder.Services.AddTransient<ISceneController, SceneController>();
		builder.Services.AddTransient<IEntityController, EntityController>();
		builder.Services.AddTransient<IComponentController, ComponentController>();
	}
}
