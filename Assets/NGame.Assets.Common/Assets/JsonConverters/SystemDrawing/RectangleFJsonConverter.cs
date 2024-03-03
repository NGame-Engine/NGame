using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NGame.Assets.Common.Assets.JsonConverters.SystemDrawing;



public class RectangleFJsonConverter : JsonConverter<RectangleF>
{
	public override RectangleF Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var stringValue = reader.GetString();
		if (stringValue == null) return new RectangleF();

		var parts = stringValue.Split(';', 4);
		var x = float.Parse(parts[0]);
		var y = float.Parse(parts[1]);
		var width = float.Parse(parts[2]);
		var height = float.Parse(parts[3]);
		return new RectangleF(x, y, width, height);
	}


	public override void Write(Utf8JsonWriter writer, RectangleF value, JsonSerializerOptions options) =>
		writer.WriteStringValue($"{value.X};{value.Y};{value.Width};{value.Height}");
}