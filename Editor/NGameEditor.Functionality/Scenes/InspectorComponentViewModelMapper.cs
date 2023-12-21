using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;
using NGameEditor.ViewModels.Components.CustomEditors;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;
using ReactiveUI;

namespace NGameEditor.Functionality.Scenes;



public class InspectorComponentViewModelMapper(
	IBackendRunner backendRunner,
	ILogger<InspectorComponentViewModelMapper> logger
) : IInspectorComponentViewModelMapper
{
	public IEnumerable<ComponentEditorViewModel> Map(EntityState entityState)
	{
		var viewModelsResult =
			backendRunner
				.GetBackendService()
				.Then(x => x.GetEditorForEntity(entityState.Id))
				.Then(x =>
					x
						.Children
						.Select(Map)
				)
				.IfError(logger.Log);

		return
			viewModelsResult.HasError
				? Array.Empty<ComponentEditorViewModel>()
				: viewModelsResult.SuccessValue!;
	}


	private ComponentEditorViewModel Map(UiElement uiElement)
	{
		if (uiElement.Type == UiElementType.StackPanel)
		{
			return new StackPanelEditorViewModel(
				uiElement
					.Children
					.Select(Map)
			);
		}

		if (uiElement.Type == UiElementType.CheckBox)
		{
			var currentSerializedValue =
				bool.TryParse(uiElement.CurrentSerializedValue, out var value) &&
				value;

			var checkBoxEditorViewModel = new CheckBoxEditorViewModel(currentSerializedValue);

			checkBoxEditorViewModel
				.WhenAnyValue(x => x.IsChecked)
				.Throttle(TimeSpan.FromMilliseconds(100))
				.ObserveOn(RxApp.MainThreadScheduler)
				.Select(x =>
					backendRunner
						.GetBackendService()
						.Then(backendService =>
							backendService
								.UpdateEditorValue(
									uiElement.Id,
									x.ToString()
								)
						)
						.IfError(logger.Log)
				)
				.Subscribe();

			return checkBoxEditorViewModel;
		}


		if (uiElement.Type == UiElementType.TextEditor)
		{
			var currentSerializedValue = uiElement.CurrentSerializedValue;

			var checkBoxEditorViewModel = new TextEditorViewModel(currentSerializedValue);

			checkBoxEditorViewModel
				.WhenAnyValue(x => x.Text)
				.Throttle(TimeSpan.FromMilliseconds(100))
				.ObserveOn(RxApp.MainThreadScheduler)
				.Select(x =>
					backendRunner
						.GetBackendService()
						.Then(backendService =>
							backendService
								.UpdateEditorValue(
									uiElement.Id,
									x
								)
						)
						.IfError(logger.Log)
				)
				.Subscribe();

			return checkBoxEditorViewModel;
		}


		return new UnrecognizedComponentEditorViewModel();
	}
}
