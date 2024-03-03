using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NGame.Assets.Common.Assets.JsonConverters.SystemDrawing;



public class PointJsonConverter : JsonConverter<Point>
{
	public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var stringValue = reader.GetString();
		if (stringValue == null) return new Point();

		var parts = stringValue.Split(';', 2);
		var x = int.Parse(parts[0]);
		var y = int.Parse(parts[1]);
		return new Point(x, y);
	}


	public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options) =>
		writer.WriteStringValue($"{value.X};{value.Y}");
}