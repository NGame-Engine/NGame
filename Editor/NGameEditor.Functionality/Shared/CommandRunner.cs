using System.Diagnostics;

namespace NGameEditor.Functionality.Shared;



public interface ICommandRunner
{
	string Run(string fileName, string arguments);
	string Run(string fileName, string arguments, string workingDirectory);
	Process RunInBackground(string fileName, string arguments, string workingDirectory);
}



public class CommandRunner : ICommandRunner
{
	public string Run(string fileName, string arguments) =>
		Run(fileName, arguments, "");


	public string Run(string fileName, string arguments, string workingDirectory)
	{
		var process = new Process
		{
			StartInfo = new ProcessStartInfo
			{
				FileName = fileName,
				Arguments = arguments,
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true,
				WorkingDirectory = workingDirectory
			}
		};

		process.Start();
		var output = process.StandardOutput.ReadToEnd();
		process.WaitForExit();

		return output;
	}


	public Process RunInBackground(string fileName, string arguments, string workingDirectory)
	{
		var process = new Process
		{
			StartInfo = new ProcessStartInfo
			{
				FileName = fileName,
				Arguments = arguments,
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true,
				WorkingDirectory = workingDirectory
			}
		};

		process.Start();

		return process;
	}
}
