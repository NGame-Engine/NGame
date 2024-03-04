using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NGame.Assets.Common.Ecs;
using NGame.Platform;
using NGame.Platform.Assets;
using NGame.Platform.Assets.Unpacking;
using NGame.Platform.Setup;
using Singulink.IO;
using Xunit.Abstractions;

namespace NGame.Assets.Packer.Tests.Integration.CanPackAndLoad;



public class CanPackAndLoadTest(ITestOutputHelper testOutputHelper)
{
	[Fact]
	public void Integration_CanCreatePackagesAndLoadThemAgain()
	{
		var projectFolderPath =
			DirectoryPath.ParseAbsolute(AppContext.BaseDirectory)
				.CombineDirectory(nameof(Integration))
				.CombineDirectory(nameof(CanPackAndLoad))
				.CombineDirectory("ExampleProject");

		var unpackedAssetsPath = projectFolderPath.CombineDirectory("AssetFolder");
		var appSettingsPath = projectFolderPath.CombineFile("appsettings.json");
		var minifyJson = true;
		var outputPath = projectFolderPath.CombineDirectory("output");

		
		var assetStreamProvider = new FileAssetStreamProvider(outputPath.PathDisplay);
		
		PackAssets(unpackedAssetsPath, appSettingsPath, minifyJson, outputPath);
		LoadAssets(assetStreamProvider);
	}


	private void PackAssets(
		IAbsoluteDirectoryPath unpackedAssetsPath,
		IFilePath appSettingsPath,
		bool minifyJson,
		IDirectoryPath outputPath
	)
	{
		var minifyJsonString = minifyJson ? "true" : "false";
	
		Program.Main([
			$"unpackedassets={unpackedAssetsPath.PathDisplay} ",
			$"appsettings={appSettingsPath.PathDisplay}",
			$"minifyJson={minifyJsonString}",
			$"output={outputPath.PathDisplay} "
		]);
	}


	private void LoadAssets(IAssetStreamProvider assetStreamProvider)
	{
		var builder = NGameBuilder.CreateDefault();


		builder.Logging.AddXUnitLogging(testOutputHelper);
		builder.AddNGamePlatform();
		builder.Services.AddSingleton(assetStreamProvider);
		builder.Services.AddSingleton(new AssetTypeEntry(typeof(SceneAsset)));


		var game = builder.Build();


		var assetFromPackReader = game.Services.GetRequiredService<IAssetAccessor>();
		var sceneAsset = assetFromPackReader.ReadFromAssetPack(Guid.Parse("0F85A235-5A85-4BFB-8BCB-2B7CAF7BF8CC"));


		// Assert
		sceneAsset.Should().NotBe(null);
	}
}
