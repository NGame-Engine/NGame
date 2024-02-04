using System.Text.Json;
using System.Text.Json.Serialization;

namespace NGame.Assets.Common.Assets;



public class AssetIdConverter : JsonConverter<AssetId>
{
	public override AssetId Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options
	)
	{
		var rawValue = reader.GetString()!;
		return AssetId.Parse(rawValue);
	}


	public override AssetId ReadAsPropertyName(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options
	) => Read(ref reader, typeToConvert, options);


	public override void Write(
		Utf8JsonWriter writer,
		AssetId value,
		JsonSerializerOptions options
	) =>
		writer.WriteStringValue(value.Id.ToString());


	public override void WriteAsPropertyName(
		Utf8JsonWriter writer,
		AssetId value,
		JsonSerializerOptions options
	) => writer.WritePropertyName(value.Id.ToString());
}
