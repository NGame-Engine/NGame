namespace NGame.OsWindows;

public interface IOsWindow
{
	event EventHandler<ResizedEventArgs> Resized;
	event EventHandler FocusLost;
	event EventHandler FocusGained;


	void Draw(byte[] pixels);
}
