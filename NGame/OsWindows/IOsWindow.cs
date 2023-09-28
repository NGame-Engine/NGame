namespace NGame.OsWindows;

public interface IOsWindow
{
	event EventHandler Closed;

	void Initialize(CancellationTokenSource cancellationTokenSource);
	void Draw(byte[] pixels);
}
