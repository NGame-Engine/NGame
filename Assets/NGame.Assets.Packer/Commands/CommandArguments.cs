namespace NGame.Assets.Packer.Commands;



public class CommandArguments
{
	public static readonly string JsonElementName = "";


	public string? UnpackedAssets { get; set; }
	public string? AppSettings { get; set; }
	public bool? MinifyJson { get; set; }
	public string? Output { get; set; }
}
