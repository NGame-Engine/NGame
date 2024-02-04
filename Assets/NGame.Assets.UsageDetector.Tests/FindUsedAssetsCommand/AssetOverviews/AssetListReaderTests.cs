using FluentAssertions;
using NGame.Assets.UsageDetector.AssetOverviews;
using Singulink.IO;

namespace NGame.Assets.UsageDetector.Tests.FindUsedAssetsCommand.AssetOverviews;



public class AssetListReaderTests
{
	private static IAssetListReader Create() => new AssetListReader();


	[Fact]
	public void ReadEntries_NormalInput_CombinesValuesCorrectly()
	{
		// Arrange
		var input = new List<string>
		{
			@"FirstPack//C:\NGameTest\MoonBalls\moon.png.ngasset",
			@"FirstPack//C:\NGameTest\Scenes\scene1.ngasset",
			@"SecondPack//C:\NGameTest\Sounds\enemy-hurt-mono.ogg.ngasset",
			@"FirstPack//C:\NGameTest\MoonBalls\moon.png",
			@"SecondPack//C:\NGameTest\Sounds\enemy-hurt-mono.ogg",
		};

		var assetListReader = Create();


		// Act
		var result = assetListReader.ReadEntries(input);


		// Assert
		var assetEntries = result.ToList();

		var moonFilePath = FilePath.ParseAbsolute(@"C:\NGameTest\MoonBalls\moon.png.ngasset");
		var moonAssetEntry = assetEntries.First(x => x.FilePath.Equals(moonFilePath));
		moonAssetEntry.PackageName.Should().Be("FirstPack");
		var moonFileCompanionPath = FilePath.Parse(@"C:\NGameTest\MoonBalls\moon.png");
		moonAssetEntry.CompanionFile.Should().Be(moonFileCompanionPath);

		var sceneFilePath = FilePath.Parse(@"C:\NGameTest\Scenes\scene1.ngasset");
		var sceneAssetEntry = assetEntries.First(x => x.FilePath.Equals(sceneFilePath));
		sceneAssetEntry.PackageName.Should().Be("FirstPack");
		sceneAssetEntry.CompanionFile.Should().Be(null);
	}
}
