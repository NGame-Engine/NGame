using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NGame.Assets;
using NGame.Core.Assets;
using NGame.Core.Assets.Readers;
using NGame.Core.Setup;
using NGame.SceneAssets;
using Xunit.Abstractions;

namespace NGame.Cli.Tests.CanPackAndLoad;

public class CanPackAndLoadTest
{
	private readonly ITestOutputHelper _testOutputHelper;


	public CanPackAndLoadTest(ITestOutputHelper testOutputHelper)
	{
		_testOutputHelper = testOutputHelper;
	}


	[Fact]
	public void Integration_CanCreatePackagesAndLoadThemAgain()
	{
		var projectFolderPath =
			Path.Combine(
				AppContext.BaseDirectory,
				nameof(CanPackAndLoad),
				"ExampleProject"
			);

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

		var cliProjectFolder = Path.Combine(solutionPath, "NGame.Cli");

		CommandLineHelper.Run(
			"dotnet",
			"build",
			cliProjectFolder,
			_testOutputHelper
		);


		var cliExePath =
			Directory.GetFiles(
					Path.Combine(cliProjectFolder, "bin"),
					"NGame.Cli*",
					SearchOption.AllDirectories
				)
				.First(x => x.EndsWith("NGame.Cli.exe") || x.EndsWith("NGame.Cli"));

		CommandLineHelper.Run(
			cliExePath,
			$"pack --assetlist={assetListPath} --project={projectFolderPath} --target={targetPath}",
			cliProjectFolder,
			_testOutputHelper
		);
	}


	private void LoadAssets(IAssetStreamProvider assetStreamProvider)
	{
		var builder = NGameBuilder.CreateDefault();


		builder.Logging.AddXUnitLogging(_testOutputHelper);
		builder.AddNGameCore();
		builder.Services.AddSingleton(assetStreamProvider);
		builder.Services.AddSingleton(new AssetTypeEntry(typeof(SceneAsset)));


		var game = builder.Build();


		var assetFromPackReader = game.Services.GetRequiredService<IAssetFromPackReader>();
		var sceneAsset = assetFromPackReader.ReadFromAssetPack(AssetId.Parse("0F85A235-5A85-4BFB-8BCB-2B7CAF7BF8CC"));


		// Assert
		sceneAsset.Should().NotBe(null);
	}
}
