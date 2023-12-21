using System.Reactive;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Results;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews.Properties;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;
using ReactiveUI;

namespace NGameEditor.Functionality.Scenes;



public class InspectorComponentViewModelMapper(
	IBackendRunner backendRunner,
	ILogger<InspectorComponentViewModelMapper> logger
) : IInspectorComponentViewModelMapper
{
	public InspectorComponentViewModel Map(ComponentState componentState, EntityState entityState)
	{
		return new InspectorComponentViewModel(
			componentState,
			state => Map(state, componentState, entityState)
		);
	}


	private PropertyViewModel Map(PropertyState propertyState, ComponentState componentState, EntityState entityState)
	{
		return new PropertyViewModel(
			propertyState,
			GetEditorViewModel(propertyState, componentState, entityState)
		);
	}


	private EditorViewModel GetEditorViewModel(
		PropertyState propertyState,
		ComponentState componentState,
		EntityState entityState
	)
	{
		var typeIdentifier = propertyState.TypeIdentifier;

		if (typeIdentifier == typeof(bool).FullName)
		{
			var checkBoxEditorViewModel = new CheckBoxEditorViewModel
			{
				Value = bool.Parse(propertyState.Value)
			};

			checkBoxEditorViewModel
				.WhenAnyValue(x => x.Value)
				.Throttle(TimeSpan.FromMilliseconds(100))
				.ObserveOn(RxApp.MainThreadScheduler)
				.Select(x =>
				{
					backendRunner
						.GetBackendService()
						.Then(backendService =>
							backendService.UpdateComponentValue(
								entityState.Id,
								componentState.Id,
								propertyState.Name,
								x?.ToString()
							)
						)
						.IfError(logger.Log);
					return Unit.Default;
				})
				.Subscribe();


			return checkBoxEditorViewModel;
		}


		var stringEditorViewModel = new StringEditorViewModel
		{
			Value = propertyState.Value
		};

		/*
		stringEditorViewModel
			.WhenAnyValue(x => x.Value)
			.Throttle(TimeSpan.FromMilliseconds(100))
			.ObserveOn(RxApp.MainThreadScheduler)
			.Select(x =>
			{
				backendRunner
					.GetBackendService()
					.Then(backendService =>
						backendService.UpdateComponentValue(
							entityState.Id,
							componentState.Id,
							propertyState.Name,
							x
						)
					)
					.IfError(logger.Log);
				return Unit.Default;
			})
			.Subscribe();
			*/

		return stringEditorViewModel;
	}
}
