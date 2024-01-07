using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.UserInterface;
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
	ILogger<AssetInspectorMapper> logger
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

		if (uiElementDto.Type == UiElementType.CheckBox)
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
