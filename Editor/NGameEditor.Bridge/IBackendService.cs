using NGameEditor.Bridge.Scenes;
using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;

namespace NGameEditor.Bridge;



public interface IBackendService
{
	SceneDescription GetLoadedScene();
	Result SaveCurrentScene();


	Result<EntityDescription> AddEntity(Guid? parentEntityId);
	Result RemoveEntity(Guid entityId);
	Result SetEntityName(Guid entityId, string newName);


	List<ComponentTypeDefinition> GetComponentTypes();


	Result<ComponentDescription> AddComponent(
		Guid entityId,
		ComponentTypeDefinition componentTypeDefinition
	);


	Result<UiElement> GetEditorForEntity(Guid entityId);


	Result UpdateEditorValue(
		Guid uiElementId,
		string? serializedNewValue
	);


	Result UpdateComponentValue(
		Guid entityId,
		Guid componentId,
		string valueName,
		string? serializedNewValue
	);
}
