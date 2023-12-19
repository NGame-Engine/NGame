namespace NGameEditor.ViewModels.Shared;



public interface IFolderPicker
{
	Task<IReadOnlyList<string>> AskUserToPickFolder(OpenOptions openOptions);



	public class OpenOptions
	{
		public string? Title { get; init; }
		public string? SuggestedStartLocation { get; init; }
		public bool AllowMultiple { get; init; }
	}
}
