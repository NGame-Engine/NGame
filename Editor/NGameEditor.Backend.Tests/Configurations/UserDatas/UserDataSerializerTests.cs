using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Backend.Configurations.UserDatas;
using NGameEditor.Backend.Setup;

namespace NGameEditor.Backend.Tests.Configurations.UserDatas;



public class UserDataSerializerTests
{
	[Fact]
	public void Deserialize_LastOpenedScene_CanDeserialize()
	{
		// Arrange
		var builder = Host.CreateApplicationBuilder();
		builder.InstallBackend(null!);
		var host = builder.Build();
		var userDataSerializer = host.Services.GetRequiredService<IUserDataSerializer>();
		var json = """{"LastOpenedScene":"0f85a235-5a85-4bfb-8bcb-2b7caf7bf8cc"}""";


		// Act
		var userData = userDataSerializer.Deserialize(json);


		// Assert
		var id = Guid.Parse("0f85a235-5a85-4bfb-8bcb-2b7caf7bf8cc");
		userData.LastOpenedScene.Should().Be(id);
	}


	[Fact]
	public void Serialize_LastOpenedScene_CanDeserializeAgain()
	{
		// Arrange
		var builder = Host.CreateApplicationBuilder();
		builder.InstallBackend(null!);
		var host = builder.Build();
		var userDataSerializer = host.Services.GetRequiredService<IUserDataSerializer>();

		var id = Guid.Parse("22365164-1356-4FB1-B1CF-92D43CE21CFF");

		var userData = new UserData
		{
			LastOpenedScene = id
		};


		// Act
		var json = userDataSerializer.Serialize(userData);
		var result = userDataSerializer.Deserialize(json);


		// Assert
		result.LastOpenedScene.Should().Be(id);
	}
}
