using Microsoft.Extensions.Configuration;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Shared;

namespace NGameEditor.Backend.Configurations;



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
			const string message = "Unable to read application configuration";
			throw new InvalidOperationException(message);
		}

		var frontendPort =
			argumentsConfiguration.FrontendPort ??
			throw new InvalidOperationException("No frontend port provided");


		var solutionPath = argumentsConfiguration.Solution;
		if (string.IsNullOrEmpty(solutionPath))
		{
			const string message = "No solution path provided";
			throw new InvalidOperationException(message);
		}

		var absoluteSolutionPath = Path.GetFullPath(solutionPath);
		if (File.Exists(absoluteSolutionPath) == false)
		{
			var message = $"Solution file '{absoluteSolutionPath}' does not exist";
			throw new InvalidOperationException(message);
		}

		var solutionFilePath = new CompatibleAbsolutePath(solutionPath);


		return new BackendApplicationArguments(frontendPort, solutionFilePath);
	}
}
