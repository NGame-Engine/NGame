using System.Diagnostics;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.Shared;

namespace NGameEditor.Bridge.InterProcessCommunication;



public interface IBackendProcessRunner
{
	Task StartNewProcess(
		AbsolutePath editorProjectFile,
		IPEndPoint ipEndPoint,
		ProjectId projectId
	);


	void StopCurrentProcess();
}



public class BackendProcessRunner : IBackendProcessRunner, IDisposable
{
	private readonly ILogger<BackendProcessRunner> _logger;


	public BackendProcessRunner(ILogger<BackendProcessRunner> logger)
	{
		_logger = logger;
	}


	private Process? Process { get; set; }


	public async Task StartNewProcess(
		AbsolutePath editorProjectFile,
		IPEndPoint ipEndPoint,
		ProjectId projectId
	)
	{
		StopCurrentProcess();

		Process = CreateProcess(editorProjectFile, ipEndPoint, projectId);


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
				_logger.LogInformation("[BackendProcess] {Output}", infoStringBuilder.ToString());
				infoStringBuilder = null;
				return;
			}

			_logger.LogInformation("[BackendProcess] {Output}", e.Data);
		}

		Process.OutputDataReceived += LogOutputData;


		Process.ErrorDataReceived += (_, e) =>
			_logger.LogError("[BackendProcess] {Output}", e.Data);


		await Process.StartAndWaitForOutput(
			BridgeConventions.ProcessStartedMessage,
			TimeSpan.FromSeconds(15)
		);
	}


	public void StopCurrentProcess()
	{
		var logString = new StringBuilder($"Stopping {nameof(BackendProcessRunner)}...");
		if (Process == null || Process.HasExited)
		{
			logString.AppendLine("already stopped");
			_logger.LogInformation("{LogString}", logString.ToString());
			return;
		}

		Process.Kill();
		Process.WaitForExit();
		Process.Dispose();

		logString.AppendLine("stopped successfully");
		_logger.LogInformation("{LogString}", logString.ToString());
	}


	void IDisposable.Dispose()
	{
		StopCurrentProcess();
	}


	private static Process CreateProcess(
		AbsolutePath editorProjectFile,
		IPEndPoint ipEndPoint,
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
					$"--port={ipEndPoint.Port}",
					$"--solution={projectId.SolutionFilePath.Path}"
				},
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				UseShellExecute = false,
				CreateNoWindow = true
			}
		};
}
