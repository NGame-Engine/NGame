using System.Diagnostics;
using Xunit.Abstractions;

namespace NGame.Assets.Packer.Tests;



public static class CommandLineHelper
{
	public static void Run(
		string fileName,
		string arguments,
		string workingDirectory,
		ITestOutputHelper testOutputHelper
	)
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
		string output = process.StandardOutput.ReadToEnd();
		process.WaitForExit();

		testOutputHelper.WriteLine(output);
	}
}
