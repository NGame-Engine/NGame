namespace NGame.Inputs;

public sealed class KeyReleasedEventArgs : EventArgs
{
	public KeyReleasedEventArgs(KeyCode keyCode)
	{
		KeyCode = keyCode;
	}


	public KeyCode KeyCode { get; }
}
