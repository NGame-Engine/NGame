namespace NGame.Inputs;

public sealed class KeyPressedEventArgs : EventArgs
{
	public KeyPressedEventArgs(KeyCode keyCode)
	{
		KeyCode = keyCode;
	}


	public KeyCode KeyCode { get; }
}
