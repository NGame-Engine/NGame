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
		var osSpecificRoot = Path.GetPathRoot(AppContext.BaseDirectory);

		var input = new List<string>
		{
			@$"FirstPack//{osSpecificRoot}NGameTest\MoonBalls\moon.png.ngasset",
			@$"FirstPack//{osSpecificRoot}NGameTest\Scenes\scene1.ngasset",
			@$"SecondPack//{osSpecificRoot}NGameTest\Sounds\enemy-hurt-mono.ogg.ngasset",
			@$"FirstPack//{osSpecificRoot}NGameTest\MoonBalls\moon.png",
			@$"SecondPack//{osSpecificRoot}NGameTest\Sounds\enemy-hurt-mono.ogg",
		};

		var assetListReader = Create();


		// Act
		var result = assetListReader.ReadEntries(input);


		// Assert
		var assetEntries = result.ToList();

		var moonFilePath = FilePath.ParseAbsolute($@"{osSpecificRoot}NGameTest\MoonBalls\moon.png.ngasset");
		var moonAssetEntry = assetEntries.First(x => x.FilePath.Equals(moonFilePath));
		moonAssetEntry.PackageName.Should().Be("FirstPack");
		var moonFileCompanionPath = FilePath.Parse($@"{osSpecificRoot}NGameTest\MoonBalls\moon.png");
		moonAssetEntry.CompanionFile.Should().Be(moonFileCompanionPath);

		var sceneFilePath = FilePath.Parse($@"{osSpecificRoot}NGameTest\Scenes\scene1.ngasset");
		var sceneAssetEntry = assetEntries.First(x => x.FilePath.Equals(sceneFilePath));
		sceneAssetEntry.PackageName.Should().Be("FirstPack");
		sceneAssetEntry.CompanionFile.Should().Be(null);
	}
}
