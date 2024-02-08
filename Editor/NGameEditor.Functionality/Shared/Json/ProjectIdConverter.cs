using System.Text.Json;
using System.Text.Json.Serialization;
using NGameEditor.Functionality.Projects;
using Singulink.IO;

namespace NGameEditor.Functionality.Shared.Json;



public class ProjectIdConverter : JsonConverter<ProjectId>
{
	public override ProjectId Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options
	) =>
		new(FilePath.ParseAbsolute(reader.GetString()!));


	public override void Write(
		Utf8JsonWriter writer,
		ProjectId value,
		JsonSerializerOptions options
	) =>
		writer.WriteStringValue(value.SolutionFilePath.PathExport);
}
