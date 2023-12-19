using System.Text.Json;

namespace NGameEditor.Functionality.Shared.Json;



public interface IEditorSerializerOptionsFactory
{
	JsonSerializerOptions Create();
}



public class EditorSerializerOptionsFactory : IEditorSerializerOptionsFactory
{
	public JsonSerializerOptions Create() =>
		new()
		{
			IncludeFields = true,
			Converters =
			{
				new ProjectIdConverter()
			}
		};
}
