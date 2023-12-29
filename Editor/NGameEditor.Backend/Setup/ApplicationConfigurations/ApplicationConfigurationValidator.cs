using System.Net;
using Microsoft.Extensions.Configuration;
using NGameEditor.Bridge.Shared;

namespace NGameEditor.Backend.Setup.ApplicationConfigurations;

public record ApplicationConfiguration(IPEndPoint IpEndPoint, AbsolutePath SolutionFilePath);


public interface IApplicationConfigurationValidator
{
	ApplicationConfiguration Validate(IConfiguration configuration);
}



public class ApplicationConfigurationValidator : IApplicationConfigurationValidator
{
	public ApplicationConfiguration Validate(IConfiguration configuration)
	{
		var argumentsConfiguration = configuration.Get<UnvalidatedApplicationConfiguration>();

		if (argumentsConfiguration == null)
		{
			var message = "Unable to read application configuration";
			throw new InvalidOperationException(message);
		}

		var port = argumentsConfiguration.Port;
		if (port == 0)
		{
			var message = "No valid port provided";
			throw new InvalidOperationException(message);
		}

		var ipEndPoint = new IPEndPoint(IPAddress.Loopback, port);


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


		return new ApplicationConfiguration(ipEndPoint, solutionFilePath);
	}
}
