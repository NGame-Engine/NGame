using NGame.Assets;
using NGame.Startup;

namespace NGame;



public static class NGameBaseInstaller
{
	public static INGameBuilder AddNGameBase(this INGameBuilder builder)
	{
		builder.AddAssetType<SceneAsset>();
		builder.AddAssetType<Font>();

		return builder;
	}
}
