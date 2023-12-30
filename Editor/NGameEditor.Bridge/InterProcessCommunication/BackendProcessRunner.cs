using System.Diagnostics;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.Shared;

namespace NGameEditor.Bridge.InterProcessCommunication;



public interface IBackendProcessRunner
{
	Task<int> StartNewProcess(
		AbsolutePath editorProjectFile,
		IPEndPoint frontendIpEndPoint,
		ProjectId projectId
	);


	void StopCurrentProcess();
}



public class BackendProcessRunner(
	ILogger<BackendProcessRunner> logger
) : IBackendProcessRunner, IDisposable
{
	private Process? Process { get; set; }


	public async Task<int> StartNewProcess(
		AbsolutePath editorProjectFile,
		IPEndPoint frontendIpEndPoint,
		ProjectId projectId
	)
	{
		StopCurrentProcess();

		Process = CreateProcess(editorProjectFile, frontendIpEndPoint, projectId);


		StringBuilder? infoStringBuilder = null;

		void LogOutputData(object sender, DataReceivedEventArgs e)
		{
			if (e.Data?.StartsWith("info") == true)
			{
				infoStringBuilder = new StringBuilder();
				infoStringBuilder.AppendLine(e.Data);
				return;
			}

			if (infoStringBuilder != null)
			{
				infoStringBuilder.AppendLine(e.Data);
				logger.LogInformation("[BackendProcess] {Output}", infoStringBuilder.ToString());
				infoStringBuilder = null;
				return;
			}

			logger.LogInformation("[BackendProcess] {Output}", e.Data);
		}

		Process.OutputDataReceived += LogOutputData;


		Process.ErrorDataReceived += (_, e) =>
			logger.LogError("[BackendProcess] {Output}", e.Data);


		var backendStartedOutput = await Process.StartAndWaitForOutput(
			output => output.Contains(BridgeConventions.ProcessStartedMessage),
			TimeSpan.FromSeconds(30)
		);

		
		var outputIndex = backendStartedOutput.IndexOf(
			BridgeConventions.ProcessStartedMessage, 
			StringComparison.OrdinalIgnoreCase
			);
		
		var portStringStart = outputIndex + BridgeConventions.ProcessStartedMessage.Length;
		
		var portString = backendStartedOutput[portStringStart..];
		var port = int.Parse(portString);
		return port;
	}


	public void StopCurrentProcess()
	{
		var logString = new StringBuilder($"Stopping {nameof(BackendProcessRunner)}...");
		if (Process == null || Process.HasExited)
		{
			logString.AppendLine("already stopped");
			logger.LogInformation("{LogString}", logString.ToString());
			return;
		}

		Process.Kill();
		Process.WaitForExit();
		Process.Dispose();

		logString.AppendLine("stopped successfully");
		logger.LogInformation("{LogString}", logString.ToString());
	}


	void IDisposable.Dispose()
	{
		StopCurrentProcess();
	}


	private static Process CreateProcess(
		AbsolutePath editorProjectFile,
		IPEndPoint frontendIpEndPoint,
		ProjectId projectId
	) =>
		new()
		{
			StartInfo = new ProcessStartInfo
			{
				FileName = "dotnet",
				ArgumentList =
				{
					"run",
					$"--project={editorProjectFile.Path}",
					"--",
					$"--frontendport={frontendIpEndPoint.Port}",
					$"--solution={projectId.SolutionFilePath.Path}"
				},
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				UseShellExecute = false,
				CreateNoWindow = true
			}
		};
}
