using System.Text.Json.Serialization;

namespace NGame.Assets;

public class Asset
{
    public string Type { get; set; }
    
    public Guid Id { get; set; }
    
    [JsonConverter(typeof(AssetVersionJsonConverter))]
    public AssetVersion SerializerVersion { get; set; }

}
