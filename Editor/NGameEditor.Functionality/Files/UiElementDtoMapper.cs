using System.Reactive.Linq;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
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
		return uiElementDto.Type switch
		{
			UiElementType.StackPanel => new StackPanelEditorViewModel(uiElementDto.Children.Select(Map)),
			UiElementType.CheckBox => CreateCheckBoxEditorViewModel(uiElementDto),
			UiElementType.Asset => CreateSelectableObjectEditorViewModel(uiElementDto),
			UiElementType.TextEditor => CreateTextEditorViewModel(uiElementDto),
			_ => new UnrecognizedCustomEditorViewModel()
		};
	}


	private CheckBoxEditorViewModel CreateCheckBoxEditorViewModel(UiElementDto uiElementDto)
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


	private SelectableObjectEditorViewModel CreateSelectableObjectEditorViewModel(UiElementDto uiElementDto)
	{
		var currentSerializedValue = uiElementDto.CurrentSerializedValue!;

		var jsonAssetInfo = JsonSerializer.Deserialize<JsonAssetInfo>(currentSerializedValue)!;

		var selectedAssetName =
			jsonAssetInfo.SelectedFilePath == null
				? "(Nothing)"
				: Path.GetFileName(jsonAssetInfo.SelectedFilePath.Path);

		var selectableObjectEditorViewModel = new SelectableObjectEditorViewModel(
			selectedAssetName,
			ReactiveCommand.Create(() =>
			{
				objectSelectorOpener.Open(jsonAssetInfo, uiElementDto);
			})
		);
		return selectableObjectEditorViewModel;
	}


	private TextEditorViewModel CreateTextEditorViewModel(UiElementDto uiElementDto)
	{
		var currentSerializedValue = uiElementDto.CurrentSerializedValue;

		var textEditorViewModel = new TextEditorViewModel(currentSerializedValue);

		textEditorViewModel
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

		return textEditorViewModel;
	}
}
