namespace NGameEditor.ViewModels.ProjectWindows.FileBrowsers;



public class FileViewModel(
	string name,
	string? assetTypeIdentifier
) : ViewModelBase
{
	public string Name { get; } = name;
	public string? AssetTypeIdentifier { get; } = assetTypeIdentifier;
}
