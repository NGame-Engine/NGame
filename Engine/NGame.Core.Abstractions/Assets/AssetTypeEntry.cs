namespace NGame.Assets;



public class AssetTypeEntry
{
	private AssetTypeEntry(Type subType)
	{
		SubType = subType;
	}


	public static AssetTypeEntry Create<TDerived>() where TDerived : Asset
		=> new(typeof(TDerived));


	public Type SubType { get; private init; }
}
