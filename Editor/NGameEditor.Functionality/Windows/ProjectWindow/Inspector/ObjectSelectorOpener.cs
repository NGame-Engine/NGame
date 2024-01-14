using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Results;
using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors.State;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.ProjectWindow.Inspector;



public interface IObjectSelectorOpener
{
	void Open(
		AssetTypeDefinition assetTypeDefinition
	);
}



public class ObjectSelectorOpener(
	IObjectSelectorWindow objectSelectorWindow,
	ObjectSelectionState objectSelectionState,
	IClientRunner<IBackendApi> clientRunner,
	ILogger<ObjectSelectorOpener> logger
) : IObjectSelectorOpener
{
	public void Open(
		AssetTypeDefinition assetTypeDefinition
	)
	{
		ClearOldObjects()
			.Then(clientRunner.GetClientService)
			.Then(x => x.GetAssetsOfType(assetTypeDefinition))
			.Then(x => x.Select(Map))
			.Then(states => SetNewObjects(states))
			.IfError(logger.Log);
	}


	private Result ClearOldObjects()
	{
		objectSelectionState.AvailableObjects.Clear();

		return Result.Success();
	}


	private SelectableObjectState Map(AssetDescription assetDescription) =>
		new(
			assetDescription.Id,
			ReactiveCommand.Create(() =>
			{
				/*clientRunner
					.GetClientService()
					.Then(x=>x.UpdateEditorValue())*/
			})
		);


	private void SetNewObjects(
		IEnumerable<SelectableObjectState> objectsForAssetType
	)
	{
		objectSelectionState.AvailableObjects.AddRange(objectsForAssetType);
		objectSelectorWindow.Open();
	}
}
