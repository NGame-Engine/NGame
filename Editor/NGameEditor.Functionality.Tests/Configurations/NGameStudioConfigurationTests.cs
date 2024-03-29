using System.Text.Json;
using NGameEditor.Functionality.Projects;
using NGameEditor.Functionality.Shared.Json;
using NGameEditor.Functionality.Users;
using Singulink.IO;

namespace NGameEditor.Functionality.Tests.Configurations;



public class NGameStudioConfigurationTests
{
	[Fact]
	public void CanDoARoundTrip()
	{
		// Arrange
		var nGameStudioConfiguration =
			new NGameStudioConfiguration
			{
				ProjectHistory =
				{
					new ProjectUsage(
						new ProjectId(FilePath.ParseAbsolute(Path.Combine(AppContext.BaseDirectory, "name1"))),
						DateTime.UnixEpoch
					),
					new ProjectUsage(
						new ProjectId(FilePath.ParseAbsolute(Path.Combine(AppContext.BaseDirectory, "name2"))),
						DateTime.UnixEpoch
					),
					new ProjectUsage(
						new ProjectId(FilePath.ParseAbsolute(Path.Combine(AppContext.BaseDirectory, "name3"))),
						DateTime.UnixEpoch
					)
				}
			};

		var jsonSerializerOptions = new EditorSerializerOptionsFactory().Create();


		// Act
		var json = JsonSerializer.Serialize(nGameStudioConfiguration, jsonSerializerOptions);
		var loadedConfiguration =
			JsonSerializer.Deserialize<NGameStudioConfiguration>(
				json,
				jsonSerializerOptions
			);


		// Assert
		Assert.NotNull(loadedConfiguration);
		Assert.Equal(3, loadedConfiguration.ProjectHistory.Count);
	}
}
