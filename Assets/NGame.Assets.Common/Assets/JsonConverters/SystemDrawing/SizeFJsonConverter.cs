using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NGame.Assets.Common.Assets.JsonConverters.SystemDrawing;



public class SizeFJsonConverter : JsonConverter<SizeF>
{
	public override SizeF Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var stringValue = reader.GetString();
		if (stringValue == null) return new SizeF();

		var parts = stringValue.Split(';', 2);
		var width = float.Parse(parts[0]);
		var height = float.Parse(parts[1]);
		return new SizeF(width, height);
	}


	public override void Write(Utf8JsonWriter writer, SizeF value, JsonSerializerOptions options) =>
		writer.WriteStringValue($"{value.Width};{value.Height}");
}
