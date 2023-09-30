namespace NGame.Inputs;

public class KeyPressedEventArgs : EventArgs
{
	public KeyPressedEventArgs(KeyCode keyCode)
	{
		KeyCode = keyCode;
	}


	public KeyCode KeyCode { get; }
}
