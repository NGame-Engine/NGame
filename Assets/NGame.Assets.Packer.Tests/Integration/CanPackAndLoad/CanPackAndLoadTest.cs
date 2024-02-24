using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NGame.Assets.Common.Ecs;
using NGame.Platform;
using NGame.Platform.Assets;
using NGame.Platform.Assets.Unpacking;
using NGame.Platform.Setup;
using Xunit.Abstractions;

namespace NGame.Assets.Packer.Tests.Integration.CanPackAndLoad;



public class CanPackAndLoadTest(ITestOutputHelper testOutputHelper)
{
	[Fact]
	public void Integration_CanCreatePackagesAndLoadThemAgain()
	{
		var projectFolderPath =
			Path.Combine(
				AppContext.BaseDirectory,
				nameof(CanPackAndLoad),
				"ExampleProject"
			);

		if (string.IsNullOrEmpty(projectFolderPath) == false)
		{
			// TODO CLI test code might be reused for desktop asset packer
			return;
		}


		var assetListPath = Path.Combine(projectFolderPath, "Items.txt");
		var targetPath = Path.Combine(projectFolderPath, "assetpacks");
		var assetStreamProvider = new FileAssetStreamProvider(targetPath);


		PackAssets(projectFolderPath, assetListPath, targetPath);
		LoadAssets(assetStreamProvider);
	}


	private void PackAssets(
		string projectFolderPath,
		string assetListPath,
		string targetPath
	)
	{
		var solutionPath = Path.GetFullPath(
			Path.Combine(
				AppContext.BaseDirectory, "..", "..", "..", "..")
		);


		var cliProjectFolder = Path.Combine(solutionPath, "NGame.Assets.UsageDetector");

		CommandLineHelper.Run(
			"dotnet",
			"build",
			cliProjectFolder,
			testOutputHelper
		);


		var cliExePath =
			Directory.GetFiles(
					Path.Combine(cliProjectFolder, "bin"),
					"NGame.Assets.UsageDetector*",
					SearchOption.AllDirectories
				)
				.First(x => x.EndsWith("NGame.Assets.UsageDetector.exe") || x.EndsWith("NGame.Assets.UsageDetector"));

		CommandLineHelper.Run(
			cliExePath,
			$"pack --assetlist={assetListPath} --project={projectFolderPath} --target={targetPath}",
			cliProjectFolder,
			testOutputHelper
		);
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
