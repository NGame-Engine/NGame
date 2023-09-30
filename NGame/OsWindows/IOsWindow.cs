namespace NGame.OsWindows;

public interface IOsWindow
{
	event EventHandler Closed;
	event EventHandler<ResizedEventArgs> Resized;
	event EventHandler FocusLost;
	event EventHandler FocusGained;


	void Initialize(CancellationTokenSource cancellationTokenSource);
	void Draw(byte[] pixels);
}
