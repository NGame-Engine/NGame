namespace NGame.Assets;

public struct AssetVersion
{
    public int Major { get; set; }
    public int Minor { get; set; }
    public int Patch { get; set; }

    public override string ToString() => $"{Major}.{Minor}.{Patch}";
}
