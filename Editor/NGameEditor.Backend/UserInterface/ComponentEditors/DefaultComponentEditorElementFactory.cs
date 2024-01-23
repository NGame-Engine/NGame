using NGame.Ecs;
using NGameEditor.Backend.UserInterface.ValueEditors;

namespace NGameEditor.Backend.UserInterface.ComponentEditors;



public interface IDefaultComponentEditorElementFactory : IComponentEditorElementFactory;



public class DefaultComponentEditorElementFactory(
	IEnumerable<IValueEditorFactory> valueEditorFactories
) : IDefaultComponentEditorElementFactory
{
	public Type Type { get; } = typeof(EntityComponent);


	public IEnumerable<EditorElement> Create(EntityComponent entityComponent)
	{
		var editablePropertyInfos =
			entityComponent
				.GetType()
				.GetProperties()
				.Where(x => x.CanRead && x.CanWrite);

		var editorElements = new List<EditorElement>();

		foreach (var propertyInfo in editablePropertyInfos)
		{
			var propertyType = propertyInfo.PropertyType;

			var factory = valueEditorFactories
				.FirstOrDefault(x => x.CanHandleType(propertyType));

			if (factory == null)
			{
				continue;
			}


			var editorElement = factory.Create(
				propertyType,
				() => propertyInfo.GetValue(entityComponent),
				x => propertyInfo.SetValue(entityComponent, x)
			);

			editorElements.Add(editorElement);
		}

		return editorElements;
	}
}
