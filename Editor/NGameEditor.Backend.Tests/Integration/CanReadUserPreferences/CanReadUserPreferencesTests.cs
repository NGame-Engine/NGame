using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Assets;
using NGame.Ecs;
using NGameEditor.Backend.Projects;
using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Backend.Setup;
using NGameEditor.Bridge.Shared;

namespace NGameEditor.Backend.Tests.Integration.CanReadUserPreferences;



public class CanReadUserPreferencesTests
{
	public class DummyComponent : EntityComponent
	{
	}



	[Fact]
	public void Integration_CanReadUserPreferences()
	{
		// Arrange
		var builder = Host.CreateApplicationBuilder();
		builder.InstallBackend(null!);

		var basePath = new AbsolutePath(AppContext.BaseDirectory);
		var solutionFolder = basePath.CombineWith(
			nameof(Integration),
			nameof(CanReadUserPreferences)
		);

		var projectDefinition = new ProjectDefinition(
			solutionFolder.CombineWith("does_not_exist.sln"),
			solutionFolder.CombineWith("does_not_exist.csproj"),
			solutionFolder.CombineWith("does_not_exist.csproj"),
			new List<Type>(),
			new List<Type> { typeof(DummyComponent) }
		);

		builder.Services.AddSingleton(projectDefinition);

		var host = builder.Build();
		var lastOpenedSceneLoader = host.Services.GetRequiredService<ILastOpenedSceneLoader>();


		// Act
		var sceneAssetResult = lastOpenedSceneLoader.GetLastOpenedScene();


		// Assert
		sceneAssetResult.ErrorValue?.Title.Should().BeNull();
		var sceneId = AssetId.Parse("0f85a235-5a85-4bfb-8bcb-2b7caf7bf8cc");
		sceneAssetResult.SuccessValue!.SceneAsset.Id.Should().Be(sceneId);
	}
}
