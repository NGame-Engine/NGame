using NGame.Ecs;

namespace NGameEditor.Backend.UserInterface.ComponentEditors;



public interface IComponentEditorElementFactory
{
	Type Type { get; }
	public IEnumerable<EditorElement> Create(EntityComponent entityComponent);
}
