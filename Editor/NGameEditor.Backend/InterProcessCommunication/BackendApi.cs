using NGameEditor.Backend.Files;
using NGameEditor.Backend.Scenes;
using NGameEditor.Backend.UserInterface;
using NGameEditor.Bridge;
using NGameEditor.Bridge.Scenes;
using NGameEditor.Bridge.Shared;
using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;

namespace NGameEditor.Backend.InterProcessCommunication;



public class BackendApi(
	ISceneSaver sceneSaver,
	ICustomEditorListener customEditorListener,
	IFileOpener fileOpener,
	IEntityController entityController,
	IComponentController componentController
)
	: IBackendApi
{
	public Result SaveCurrentScene() => Result.Try(sceneSaver.SaveCurrentScene);


	public Result OpenFile(AbsolutePath filePath) =>
		Result.Try(() => fileOpener.Open(filePath));


	public Result<EntityDescription> AddEntity(Guid? parentEntityId) =>
		Result.Try(() => entityController.AddEntity(parentEntityId));


	public Result RemoveEntity(Guid entityId) =>
		Result.Try(() => entityController.RemoveEntity(entityId));


	public Result SetEntityName(Guid entityId, string newName) =>
		Result.Try(() => entityController.SetEntityName(entityId, newName));


	public Result<ComponentDescription> AddComponent(
		Guid entityId,
		ComponentTypeDefinition componentTypeDefinition
	) =>
		Result.Try(() => componentController.AddComponent(entityId, componentTypeDefinition));


	public Result<UiElementDto> GetEditorForAsset(AbsolutePath filePath) =>
		Result.Try(() => customEditorListener.GetEditorForFile(filePath));


	public Result<UiElementDto> GetEditorForEntity(Guid entityId) =>
		Result.Try(() => customEditorListener.GetEditorForEntity(entityId));


	public Result UpdateEditorValue(Guid uiElementId, string? serializedNewValue) =>
		Result.Try(() => customEditorListener.UpdateEditorValue(uiElementId, serializedNewValue));
}
