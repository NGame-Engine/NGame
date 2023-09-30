namespace NGame.Inputs;

public class KeyReleasedEventArgs : EventArgs
{
	public KeyReleasedEventArgs(KeyCode keyCode)
	{
		KeyCode = keyCode;
	}


	public KeyCode KeyCode { get; }
}
