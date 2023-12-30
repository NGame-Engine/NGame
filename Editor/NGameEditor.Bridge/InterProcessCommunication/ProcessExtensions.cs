using System.Diagnostics;

namespace NGameEditor.Bridge.InterProcessCommunication;



public static class ProcessExtensions
{
	public static Task<string> StartAndWaitForOutput(
		this Process process,
		Func<string, bool> outputIsMatch,
		TimeSpan timeout
	)
	{
		var taskCompletionSource = new TaskCompletionSource<string>();

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
			if (e.Data == null || outputIsMatch(e.Data) == false) return;

			process.OutputDataReceived -= ListenForBackendStart;
			taskCompletionSource.TrySetResult(e.Data);
		}
	}
}
