using System.Diagnostics;

namespace NGameEditor.Bridge.InterProcessCommunication;



public static class ProcessExtensions
{
	public static Task StartAndWaitForOutput(
		this Process process,
		string output,
		TimeSpan timeout
	)
	{
		var taskCompletionSource = new TaskCompletionSource();

		new CancellationTokenSource(timeout)
			.Token
			.Register(
				() => taskCompletionSource.TrySetCanceled(),
				false
			);

		process.OutputDataReceived += ListenForBackendStart;
		process.Start();
		
		process.BeginOutputReadLine();
		process.BeginErrorReadLine();

		return taskCompletionSource.Task;

		void ListenForBackendStart(object sender, DataReceivedEventArgs e)
		{
			if (e.Data?.Contains(output) != true) return;

			process.OutputDataReceived -= ListenForBackendStart;
			taskCompletionSource.TrySetResult();
		}
	}
}
