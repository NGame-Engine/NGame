using Microsoft.Extensions.Configuration;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Shared;

namespace NGameEditor.Backend.Setup.ApplicationConfigurations;



public interface IApplicationConfigurationValidator
{
	BackendApplicationArguments Validate(IConfiguration configuration);
}



public class ApplicationConfigurationValidator : IApplicationConfigurationValidator
{
	public BackendApplicationArguments Validate(IConfiguration configuration)
	{
		var argumentsConfiguration = configuration.Get<UnvalidatedApplicationConfiguration>();

		if (argumentsConfiguration == null)
		{
			var message = "Unable to read application configuration";
			throw new InvalidOperationException(message);
		}

		var frontendPort =
			argumentsConfiguration.FrontendPort ??
			throw new InvalidOperationException("No frontend port provided");


		var solutionPath = argumentsConfiguration.Solution;
		if (string.IsNullOrEmpty(solutionPath))
		{
			var message = "No solution path provided";
			throw new InvalidOperationException(message);
		}

		var absoluteSolutionPath = Path.GetFullPath(solutionPath);
		if (File.Exists(absoluteSolutionPath) == false)
		{
			var message = $"Solution file '{absoluteSolutionPath}' does not exist";
			throw new InvalidOperationException(message);
		}

		var solutionFilePath = new AbsolutePath(solutionPath);


		return new BackendApplicationArguments(frontendPort, solutionFilePath);
	}
}
