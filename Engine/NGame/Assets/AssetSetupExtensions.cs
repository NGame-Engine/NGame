using Microsoft.Extensions.DependencyInjection;
using NGame.Setup;

namespace NGame.Assets;



public static class AssetSetupExtensions
{
	public static INGameBuilder RegisterAssetType<T>(this INGameBuilder builder)
		where T : Asset
	{
		builder.Services.AddSingleton(AssetTypeEntry.Create<T>());

		return builder;
	}
}
