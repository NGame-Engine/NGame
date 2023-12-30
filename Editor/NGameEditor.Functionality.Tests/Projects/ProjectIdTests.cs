using FluentAssertions;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Projects;
using NGameEditor.Bridge.Shared;

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
			new AbsolutePath(
				Path.Combine(
					localRoot,
					"some_folder",
					"solution_folder"
				)
			)
		);
	}
}
