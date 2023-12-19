using System.Text.Json;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Shared;
using NGameEditor.Functionality.Shared.Json;
using NGameEditor.Functionality.Users;

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
						new ProjectId(new AbsolutePath("C:/name1")),
						DateTime.UnixEpoch
					),
					new ProjectUsage(
						new ProjectId(new AbsolutePath("C:/name2")),
						DateTime.UnixEpoch
					),
					new ProjectUsage(
						new ProjectId(new AbsolutePath("C:/name3")),
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
