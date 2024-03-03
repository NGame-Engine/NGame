using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NGame.Assets.Common.Assets.JsonConverters.SystemDrawing;



public class SizeJsonConverter : JsonConverter<Size>
{
	public override Size Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var stringValue = reader.GetString();
		if (stringValue == null) return new Size();

		var parts = stringValue.Split(';', 2);
		var width = int.Parse(parts[0]);
		var height = int.Parse(parts[1]);
		return new Size(width, height);
	}


	public override void Write(Utf8JsonWriter writer, Size value, JsonSerializerOptions options) =>
		writer.WriteStringValue($"{value.Width};{value.Height}");
}