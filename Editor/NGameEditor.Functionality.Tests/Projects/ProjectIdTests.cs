using FluentAssertions;
using NGameEditor.Bridge.Projects;
using NGameEditor.Bridge.Shared;
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

		var absolutePath = new AbsolutePath(configFilePath);
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
