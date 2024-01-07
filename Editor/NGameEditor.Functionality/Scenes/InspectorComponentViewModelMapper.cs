using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Functionality.Files;
using NGameEditor.Functionality.Scenes.State;
using NGameEditor.Results;
using NGameEditor.ViewModels.Components.CustomEditors;

namespace NGameEditor.Functionality.Scenes;



public interface IInspectorComponentViewModelMapper
{
	IEnumerable<CustomEditorViewModel> Map(EntityState entityState);
}



public class InspectorComponentViewModelMapper(
	IClientRunner<IBackendApi> clientRunner,
	ILogger<InspectorComponentViewModelMapper> logger,
	IUiElementDtoMapper uiElementDtoMapper
)
	: IInspectorComponentViewModelMapper
{
	public IEnumerable<CustomEditorViewModel> Map(EntityState entityState)
	{
		var viewModelsResult =
			clientRunner
				.GetClientService()
				.Then(x => x.GetEditorForEntity(entityState.Id))
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
