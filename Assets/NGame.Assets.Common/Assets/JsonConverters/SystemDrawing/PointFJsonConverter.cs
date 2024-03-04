using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NGame.Assets.Common.Assets.JsonConverters.SystemDrawing;



public class PointFJsonConverter : JsonConverter<PointF>
{
	public override PointF Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var stringValue = reader.GetString();
		if (stringValue == null) return new PointF();

		var parts = stringValue.Split(';', 2);
		var x = float.Parse(parts[0]);
		var y = float.Parse(parts[1]);
		return new PointF(x, y);
	}


	public override void Write(Utf8JsonWriter writer, PointF value, JsonSerializerOptions options) =>
		writer.WriteStringValue($"{value.X};{value.Y}");
}
