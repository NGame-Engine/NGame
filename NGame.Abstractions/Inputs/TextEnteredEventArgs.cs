namespace NGame.Inputs;

public sealed class TextEnteredEventArgs : EventArgs
{
	public TextEnteredEventArgs(string text)
	{
		Text = text;
	}


	public string Text { get; }
}
