using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;
using NGameEditor.ViewModels.Components.CustomEditors;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;

namespace NGameEditor.Functionality.Files;



public interface IAssetInspectorMapper
{
	IEnumerable<CustomEditorViewModel> Map(DirectoryContentItemViewModel directoryContentItemViewModel);
}



public class AssetInspectorMapper(
	IClientRunner<IBackendApi> clientRunner,
	ILogger<AssetInspectorMapper> logger,
	IUiElementDtoMapper uiElementDtoMapper
) : IAssetInspectorMapper
{
	public IEnumerable<CustomEditorViewModel> Map(DirectoryContentItemViewModel directoryContentItemViewModel)
	{
		var path = new CompatibleAbsolutePath(directoryContentItemViewModel.Name);

		var viewModelsResult =
			clientRunner
				.GetClientService()
				.Then(x => x.GetEditorForAsset(path))
				.Then(x =>
					x
						.Children
						.Select(uiElementDtoMapper.Map)
				)
				.IfError(logger.Log);

		return
			viewModelsResult.HasError
				? Array.Empty<CustomEditorViewModel>()
				: viewModelsResult.SuccessValue!;
	}
}
