using NGameEditor.Bridge.Scenes;
using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;

namespace NGameEditor.Bridge;



public interface IBackendApi
{
	Result SaveCurrentScene();


	Result<EntityDescription> AddEntity(Guid? parentEntityId);
	Result RemoveEntity(Guid entityId);
	Result SetEntityName(Guid entityId, string newName);


	Result<ComponentDescription> AddComponent(
		Guid entityId,
		ComponentTypeDefinition componentTypeDefinition
	);


	Result<UiElementDto> GetEditorForEntity(Guid entityId);
	Result UpdateEditorValue(Guid uiElementId, string? serializedNewValue);
}
