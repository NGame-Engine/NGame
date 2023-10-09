namespace NGame.Services;

public sealed class KeyPressedEventArgs : EventArgs
{
	public KeyPressedEventArgs(KeyCode keyCode)
	{
		KeyCode = keyCode;
	}


	public KeyCode KeyCode { get; }
}
