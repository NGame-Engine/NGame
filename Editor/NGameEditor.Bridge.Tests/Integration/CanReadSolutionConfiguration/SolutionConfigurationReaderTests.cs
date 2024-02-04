using FluentAssertions;
using NGameEditor.Bridge.Setup;
using Singulink.IO;

namespace NGameEditor.Bridge.Tests.Integration.CanReadSolutionConfiguration;



public class SolutionConfigurationReaderTests
{
	[Fact]
	public void Integration_CanReadSolutionConfiguration()
	{
		// Arrange
		var solutionConfigurationReader = new SolutionConfigurationReader();
		var baseDirectory = AppContext.BaseDirectory;
		var absoluteBaseDirectory = DirectoryPath.ParseAbsolute(baseDirectory);
		var solutionFilePath = absoluteBaseDirectory
			.CombineDirectory(nameof(Integration))
			.CombineDirectory(nameof(CanReadSolutionConfiguration))
			.CombineFile("Example.sln");


		// Act
		var result = solutionConfigurationReader.Read(solutionFilePath);


		// Assert
		result.HasError.Should().BeFalse();
		var jsonModel = result.SuccessValue!;
		jsonModel.GameProjectFile.Should().Be("Example.Game/Example.Game.csproj");
		jsonModel.EditorProjectFile.Should().Be("Example.Game.Editor/Example.Game.Editor.csproj");
	}
}
