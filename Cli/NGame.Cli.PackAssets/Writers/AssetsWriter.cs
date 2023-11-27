using Microsoft.Extensions.Logging;
using NGame.Cli.PackAssets.CommandValidators;
using NGame.Cli.PackAssets.Specifications;

namespace NGame.Cli.PackAssets.Writers;



public interface IAssetsWriter
{
	void Write(
		ValidatedCommand validatedCommand,
		ICollection<AssetPackSpecification> assetPackSpecifications
	);
}



public class AssetsWriter : IAssetsWriter
{
	private readonly ILogger<AssetsWriter> _logger;
	private readonly IAssetPackSpecificationWriter _assetPackSpecificationWriter;
	private readonly ITableOfContentsGenerator _tableOfContentsGenerator;
	private readonly ITableOfContentsWriter _tableOfContentsWriter;


	public AssetsWriter(
		ILogger<AssetsWriter> logger,
		IAssetPackSpecificationWriter assetPackSpecificationWriter,
		ITableOfContentsGenerator tableOfContentsGenerator,
		ITableOfContentsWriter tableOfContentsWriter
	)
	{
		_logger = logger;
		_assetPackSpecificationWriter = assetPackSpecificationWriter;
		_tableOfContentsGenerator = tableOfContentsGenerator;
		_tableOfContentsWriter = tableOfContentsWriter;
	}


	public void Write(
		ValidatedCommand validatedCommand,
		ICollection<AssetPackSpecification> assetPackSpecifications
	)
	{
		var targetFolder = validatedCommand.TargetFolder;
		Directory.CreateDirectory(targetFolder.Value);

		foreach (var assetPackSpecification in assetPackSpecifications)
		{
			_assetPackSpecificationWriter.Write(assetPackSpecification, targetFolder);
		}


		var tableOfContents = _tableOfContentsGenerator.CreateTableOfContents(assetPackSpecifications);
		_tableOfContentsWriter.Write(tableOfContents, validatedCommand.TargetFolder);
		_logger.LogInformation("Asset table of contents written");
	}
}
