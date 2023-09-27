namespace NGame.OsWindows;

public interface IOsWindow
{
	event EventHandler Closed;

	object? RenderTexture { get; }

	void Initialize(CancellationTokenSource cancellationTokenSource);
	void Draw();
}
