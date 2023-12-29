using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Backend.Configurations.UserDatas;

namespace NGameEditor.Backend.Configurations;



public static class ConfigurationsInstaller
{
	public static void AddConfigurations(this IHostApplicationBuilder builder)
	{
		builder.Services.Configure<SolutionStructure>(
			builder.Configuration.GetSection(SolutionStructure.JsonSectionName)
		);


		builder.Services.AddTransient<IUserDataSerializer, UserDataSerializer>();
		builder.Services.AddTransient<IGameConfigurationService, GameConfigurationService>();
		builder.Services.AddTransient<IJsonSectionUpdater, JsonSectionUpdater>();
		builder.Services.AddTransient<IEditorConfigurationService, EditorConfigurationService>();
	}
}
