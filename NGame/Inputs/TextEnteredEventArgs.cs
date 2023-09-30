namespace NGame.Inputs;

public class TextEnteredEventArgs : EventArgs
{
	public TextEnteredEventArgs(string text)
	{
		Text = text;
	}


	public string Text { get; }
}
