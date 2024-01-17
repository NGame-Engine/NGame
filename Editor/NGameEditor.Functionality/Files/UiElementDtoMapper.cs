using System.Reactive.Linq;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Shared;
using NGameEditor.Bridge.UserInterface;
using NGameEditor.Functionality.Windows.ProjectWindow.Inspector;
using NGameEditor.Results;
using NGameEditor.ViewModels.Components.CustomEditors;
using ReactiveUI;

namespace NGameEditor.Functionality.Files;



public interface IUiElementDtoMapper
{
	CustomEditorViewModel Map(UiElementDto uiElementDto);
}



public class UiElementDtoMapper(
	IClientRunner<IBackendApi> clientRunner,
	ILogger<AssetInspectorMapper> logger,
	IObjectSelectorOpener objectSelectorOpener
) : IUiElementDtoMapper
{
	public CustomEditorViewModel Map(UiElementDto uiElementDto)
	{
		if (uiElementDto.Type == UiElementType.StackPanel)
		{
			return new StackPanelEditorViewModel(
				uiElementDto
					.Children
					.Select(Map)
			);
		}

		if (uiElementDto.Type == UiElementType.CheckBox) // TODO handle UiElementTypes differently 
		{
			var currentSerializedValue =
				bool.TryParse(uiElementDto.CurrentSerializedValue, out var value) &&
				value;

			var checkBoxEditorViewModel = new CheckBoxEditorViewModel(currentSerializedValue);

			checkBoxEditorViewModel
				.WhenAnyValue(x => x.IsChecked)
				.Throttle(TimeSpan.FromMilliseconds(100))
				.ObserveOn(RxApp.MainThreadScheduler)
				.Select(x =>
					clientRunner
						.GetClientService()
						.Then(backendService =>
							backendService
								.UpdateEditorValue(
									uiElementDto.Id,
									x.ToString()
								)
						)
						.IfError(logger.Log)
				)
				.Subscribe();

			return checkBoxEditorViewModel;
		}


		if (uiElementDto.Type == UiElementType.Asset)
		{
			var currentSerializedValue = uiElementDto.CurrentSerializedValue!;
			
			
			
			
			var jsonAssetInfo = JsonSerializer.Deserialize<JsonAssetInfo>(currentSerializedValue)!;
			
			
			

			var selectedAssetName =
				jsonAssetInfo.SelectedFilePath == null
					? "(Nothing)"
					: Path.GetFileName(jsonAssetInfo.SelectedFilePath);

			var assetTypeDefinition = new AssetTypeDefinition(
				jsonAssetInfo.TypeName,
				jsonAssetInfo.TypeIdentifier,
				false // TODO get actual recognized value
			);

			var checkBoxEditorViewModel = new SelectableObjectEditorViewModel(
				selectedAssetName,
				ReactiveCommand.Create(() =>
				{
					objectSelectorOpener.Open(assetTypeDefinition);
				})
			);


			return checkBoxEditorViewModel;
		}

		if (uiElementDto.Type == UiElementType.TextEditor)
		{
			var currentSerializedValue = uiElementDto.CurrentSerializedValue;

			var checkBoxEditorViewModel = new TextEditorViewModel(currentSerializedValue);

			checkBoxEditorViewModel
				.WhenAnyValue(x => x.Text)
				.Throttle(TimeSpan.FromMilliseconds(100))
				.ObserveOn(RxApp.MainThreadScheduler)
				.Select(x =>
					clientRunner
						.GetClientService()
						.Then(backendService =>
							backendService
								.UpdateEditorValue(
									uiElementDto.Id,
									x
								)
						)
						.IfError(logger.Log)
				)
				.Subscribe();

			return checkBoxEditorViewModel;
		}


		return new UnrecognizedCustomEditorViewModel();
	}
}
