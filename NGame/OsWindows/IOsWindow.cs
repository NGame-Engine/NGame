namespace NGame.OsWindows;

public interface IOsWindow
{
	event EventHandler Closed;
	IntPtr PixelsPointer { get; }

	void Initialize(CancellationTokenSource cancellationTokenSource);
	void Draw(byte[] pixels);
}
