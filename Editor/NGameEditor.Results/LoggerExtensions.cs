using Microsoft.Extensions.Logging;

namespace NGameEditor.Results;



public static class LoggerExtensions
{
	public static void Log(this ILogger logger, Error error)
	{
		if (string.IsNullOrEmpty(error.Description))
		{
			logger.LogError("{Title}", error.Title);
			return;
		}

		logger.LogError(
			"{Title}{NewLine}{Description}",
			error.Title,
			Environment.NewLine,
			error.Description
		);
	}
}
