using FluentAssertions;
using NGameEditor.Functionality.Projects;
using Singulink.IO;

namespace NGameEditor.Functionality.Tests.Projects;



public class ProjectIdTests
{
	[Fact]
	public void GetAbsoluteSolutionFolder_ReturnsCorrectFolder()
	{
		// Arrange
		var localRoot = Path.GetPathRoot(AppContext.BaseDirectory)!;

		var configFilePath = DirectoryPath
			.ParseAbsolute(localRoot)
			.CombineDirectory("some_folder")
			.CombineDirectory("solution_folder")
			.CombineFile("solution.sln");

		var projectId = new ProjectId(configFilePath);


		// Act
		var result = projectId.GetAbsoluteSolutionFolder();


		// Assert
		result.Should().Be(
			DirectoryPath
				.ParseAbsolute(localRoot)
				.CombineDirectory("some_folder")
				.CombineDirectory("solution_folder")
		);
	}
}
