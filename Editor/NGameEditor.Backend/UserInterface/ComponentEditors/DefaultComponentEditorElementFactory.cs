using NGame.Ecs;
using NGameEditor.Backend.UserInterface.ValueEditors;

namespace NGameEditor.Backend.UserInterface.ComponentEditors;



public interface IDefaultComponentEditorElementFactory : IComponentEditorElementFactory;



public class DefaultComponentEditorElementFactory(
	IEnumerable<IValueEditorFactory> valueEditorFactories
) : IDefaultComponentEditorElementFactory
{
	private readonly Dictionary<Type, IValueEditorFactory> _valueEditorFactories =
		valueEditorFactories.ToDictionary(x => x.ValueType);


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

			if (_valueEditorFactories.TryGetValue(propertyType, out var factory) == false)
			{
				continue;
			}


			var editorElement = factory.Create(
				propertyInfo.GetValue(entityComponent),
				x => propertyInfo.SetValue(entityComponent, x)
			);

			editorElements.Add(editorElement);
		}

		return editorElements;
	}
}
