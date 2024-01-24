using System.Text.Json;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;
using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors.State;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.ProjectWindow.Inspector;



public interface IObjectSelectorOpener
{
	void Open(JsonAssetInfo jsonAssetInfo, UiElementDto uiElementDto);
}



public class ObjectSelectorOpener(
	IObjectSelectorWindow objectSelectorWindow,
	ObjectSelectionState objectSelectionState,
	IClientRunner<IBackendApi> clientRunner,
	ILogger<ObjectSelectorOpener> logger
) : IObjectSelectorOpener
{
	public void Open(JsonAssetInfo jsonAssetInfo, UiElementDto uiElementDto)
	{
		var assetTypeDefinition = new AssetTypeDefinition(
			jsonAssetInfo.TypeName,
			jsonAssetInfo.TypeIdentifier
		);

		ClearOldObjects()
			.Then(clientRunner.GetClientService)
			.Then(x => x.GetAssetsOfType(assetTypeDefinition))
			.Then(x =>
				x.Select(description =>
					Map(description, jsonAssetInfo, uiElementDto)
				)
			)
			.Then(SetNewObjects)
			.IfError(logger.Log);
	}


	private Result ClearOldObjects()
	{
		objectSelectionState.AvailableObjects.Clear();

		return Result.Success();
	}


	private SelectableObjectState Map(
		AssetDescription assetDescription,
		JsonAssetInfo jsonAssetInfo,
		UiElementDto uiElementDto
	) =>
		new(
			assetDescription.Id,
			ReactiveCommand.Create(() =>
			{
				logger.LogInformation("Asset desc open: {FilePath}", assetDescription.FilePath.Path);

				jsonAssetInfo.SelectedFilePath = assetDescription.FilePath;
				var serializedValue = JsonSerializer.Serialize(jsonAssetInfo);

				clientRunner
					.GetClientService()
					.Then(x =>
						x.UpdateEditorValue(uiElementDto.Id, serializedValue)
					);

				objectSelectorWindow.Hide();
			})
		)
		{
			Name = assetDescription.Name,
			KindName = assetDescription.AssetTypeDefinition.Name,
			Path = assetDescription.FilePath.Path
		};


	private void SetNewObjects(
		IEnumerable<SelectableObjectState> objectsForAssetType
	)
	{
		objectSelectionState.AvailableObjects.AddRange(objectsForAssetType);
		objectSelectorWindow.Open();
	}
}
