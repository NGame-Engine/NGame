namespace NGameEditor.ViewModels.Shared;



public interface IFilePicker
{
	Task<IReadOnlyList<string>> AskUserToPickFile(OpenOptions openOptions);



	public sealed class FileType
	{
		public string Name { get; init; } = "";
		public IReadOnlyList<string>? Patterns { get; init; }
	}



	public class OpenOptions
	{
		public string? Title { get; init; }
		public string? SuggestedStartLocation { get; init; }
		public bool AllowMultiple { get; init; }
		public IReadOnlyList<FileType>? FileTypeFilter { get; init; }
	}
}
