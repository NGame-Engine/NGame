using Microsoft.Extensions.Configuration;

namespace NGame.Setup;



public static class PlatformConfigurationInstaller
{
	public static IConfigurationBuilder ConfigurePlatform(
		this IConfigurationBuilder configurationBuilder,
		PlatformConfiguration platformConfiguration
	)
	{
		var rootKey = PlatformConfiguration.ConfigKey;

		var osKey = $"{rootKey}:{nameof(PlatformConfiguration.NgOperatingSystem)}";

		var versionKey = $"{rootKey}:{nameof(PlatformConfiguration.Version)}";
		var versionMajorKey = $"{versionKey}:{nameof(OperatingSystemVersion.Major)}";
		var versionMinorKey = $"{versionKey}:{nameof(OperatingSystemVersion.Minor)}";

		configurationBuilder.AddInMemoryCollection(
			new Dictionary<string, string?>
			{
				[osKey] = $"{(int)platformConfiguration.NgOperatingSystem}",
				[versionMajorKey] = $"{platformConfiguration.Version.Major}",
				[versionMinorKey] = $"{platformConfiguration.Version.Minor}"
			}
		);

		return configurationBuilder;
	}
}
