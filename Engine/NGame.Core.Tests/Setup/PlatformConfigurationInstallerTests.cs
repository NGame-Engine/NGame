using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NGame.Core.Setup;
using NGame.Setup;

namespace NGame.Core.Tests.Setup;



public class PlatformConfigurationInstallerTests
{
	[Fact]
	public void AddPlatform_NormalValues_CanBeGottenAsSection()
	{
		// Arrange
		var configurationManager = new ConfigurationManager();

		configurationManager.ConfigurePlatform(
			new PlatformConfiguration
			{
				NgOperatingSystem = NgOperatingSystem.Linux,
				Version = new OperatingSystemVersion
				{
					Major = 3,
					Minor = 7
				}
			}
		);


		// Act
		var platformConfiguration =
			configurationManager
				.GetSection(PlatformConfiguration.ConfigKey)
				.Get<PlatformConfiguration>();


		// Assert
		platformConfiguration.Should().NotBe(null);
		platformConfiguration!.NgOperatingSystem.Should().Be(NgOperatingSystem.Linux);
		platformConfiguration.Version.Major.Should().Be(3);
		platformConfiguration.Version.Minor.Should().Be(7);
	}


	[Fact]
	public void AddPlatform_NormalValues_CanBeGottenAsIOptions()
	{
		// Arrange
		var builder = Host.CreateApplicationBuilder();

		builder.Configuration.ConfigurePlatform(
			new PlatformConfiguration
			{
				NgOperatingSystem = NgOperatingSystem.Linux,
				Version = new OperatingSystemVersion
				{
					Major = 3,
					Minor = 7
				}
			}
		);

		builder.Services.Configure<PlatformConfiguration>(
			builder.Configuration.GetSection(PlatformConfiguration.ConfigKey));

		var host = builder.Build();


		// Act
		var platformConfiguration =
			host
				.Services
				.GetRequiredService<IOptions<PlatformConfiguration>>()
				.Value;


		// Assert
		platformConfiguration.Should().NotBe(null);
		platformConfiguration!.NgOperatingSystem.Should().Be(NgOperatingSystem.Linux);
		platformConfiguration.Version.Major.Should().Be(3);
		platformConfiguration.Version.Minor.Should().Be(7);
	}
}
