using System.Text.Json;
using System.Text.Json.Serialization;

namespace NGame.Assets;

public class AssetVersionJsonConverter : JsonConverter<AssetVersion>
{
    public override AssetVersion Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var numbers = reader
            .GetString()!
            .Split('.')
            .Select(int.Parse)
            .ToList();

        return new AssetVersion
        {
            Major = numbers[0],
            Minor = numbers[1],
            Patch = numbers[2]
        };
    }


    public override void Write(
        Utf8JsonWriter writer,
        AssetVersion version,
        JsonSerializerOptions options) =>
        writer.WriteStringValue($"{version.Major}.{version.Minor}.{version.Patch}");
}
