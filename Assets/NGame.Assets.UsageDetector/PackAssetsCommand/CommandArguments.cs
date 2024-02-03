using Microsoft.Extensions.Hosting;

namespace NGame.Cli.PackAssetsCommand;



public class CommandArguments
{
	public static readonly string JsonElementName = "";


	public string? AssetList { get; set; }
	public string? Project { get; set; }
	public string? Target { get; set; }
	public string Environment { get; set; } = Environments.Development;
}
