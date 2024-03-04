using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NGame.Assets.Common.Assets.JsonConverters.SystemDrawing;



public class ColorJsonConverter : JsonConverter<Color>
{
	public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var stringValue = reader.GetString();
		if (stringValue == null) return new Color();

		var parts = stringValue.Split(';', 4);
		var red = byte.Parse(parts[0]);
		var green = byte.Parse(parts[1]);
		var blue = byte.Parse(parts[2]);
		var alpha = byte.Parse(parts[3]);
		return Color.FromArgb(alpha, red, green, blue);
	}


	public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options) =>
		writer.WriteStringValue($"{value.R};{value.G};{value.B};{value.A}");
}
