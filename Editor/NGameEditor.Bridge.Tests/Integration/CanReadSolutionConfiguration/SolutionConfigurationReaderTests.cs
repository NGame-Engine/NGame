using FluentAssertions;
using NGameEditor.Bridge.Setup;
using NGameEditor.Bridge.Shared;

namespace NGameEditor.Bridge.Tests.Integration.CanReadSolutionConfiguration;



public class SolutionConfigurationReaderTests
{
	[Fact]
	public void Integration_CanReadSolutionConfiguration()
	{
		// Arrange
		var solutionConfigurationReader = new SolutionConfigurationReader();
		var baseDirectory = AppContext.BaseDirectory;
		var absoluteBaseDirectory = new AbsolutePath(baseDirectory);
		var solutionFilePath = absoluteBaseDirectory
			.CombineWith(
				nameof(Integration),
				nameof(CanReadSolutionConfiguration),
				"Example.sln"
			);


		// Act
		var result = solutionConfigurationReader.Read(solutionFilePath);


		// Assert
		result.HasError.Should().BeFalse();
		var jsonModel = result.SuccessValue!;
		jsonModel.GameProjectFile.Should().Be("Example.Game/Example.Game.csproj");
		jsonModel.EditorProjectFile.Should().Be("Example.Game.Editor/Example.Game.Editor.csproj");
	}
}
