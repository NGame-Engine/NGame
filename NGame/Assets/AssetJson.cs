using System.Text.Json.Serialization;

namespace NGame.Assets;

public abstract class AssetJson
{
	public Guid Id { get; set; }

	[JsonConverter(typeof(AssetVersionJsonConverter))]
	public AssetVersion SerializerVersion { get; set; }
}
