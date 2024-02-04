using FluentAssertions;
using NGameEditor.Bridge.Projects;
using Singulink.IO;

namespace NGameEditor.Functionality.Tests.Projects;



public class ProjectIdTests
{
	[Fact]
	public void GetAbsoluteSolutionFolder_ReturnsCorrectFolder()
	{
		// Arrange

		var localRoot = Path.GetPathRoot(AppContext.BaseDirectory)!;
		var configFilePath =
			Path.Combine(
				localRoot,
				"some_folder",
				"solution_folder",
				"solution.sln"
			);

		var absolutePath = FilePath.ParseAbsolute(configFilePath);
		var projectId = new ProjectId(absolutePath);


		// Act
		var result = projectId.GetAbsoluteSolutionFolder();


		// Assert
		result.Should().Be(
			FilePath.ParseAbsolute(
				Path.Combine(
					localRoot,
					"some_folder",
					"solution_folder"
				)
			)
		);
	}
}
