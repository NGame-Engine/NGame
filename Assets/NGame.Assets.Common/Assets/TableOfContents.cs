namespace NGame.Assets.Common.Assets;



public class TableOfContents
{
	public Dictionary<Guid, ContentEntry> ResourceIdentifiers { get; init; } = new();
}



public class ContentEntry
{
	public string PackFileName { get; init; } = null!;
	public string FilePath { get; init; } = null!;
}
