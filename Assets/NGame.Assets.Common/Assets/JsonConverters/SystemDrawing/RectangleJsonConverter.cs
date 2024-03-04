using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NGame.Assets.Common.Assets.JsonConverters.SystemDrawing;



public class RectangleJsonConverter : JsonConverter<Rectangle>
{
	public override Rectangle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var stringValue = reader.GetString();
		if (stringValue == null) return new Rectangle();

		var parts = stringValue.Split(';', 4);
		var x = int.Parse(parts[0]);
		var y = int.Parse(parts[1]);
		var width = int.Parse(parts[2]);
		var height = int.Parse(parts[3]);
		return new Rectangle(x, y, width, height);
	}


	public override void Write(Utf8JsonWriter writer, Rectangle value, JsonSerializerOptions options) =>
		writer.WriteStringValue($"{value.X};{value.Y};{value.Width};{value.Height}");
}
