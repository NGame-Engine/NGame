namespace NGameEditor.Results;



public class Error(string title, string description)
{
	public string Title { get; } = title;
	public string Description { get; } = description;
}
