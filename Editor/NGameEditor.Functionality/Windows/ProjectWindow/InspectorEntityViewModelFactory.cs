using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Functionality.Scenes;
using NGameEditor.Functionality.Scenes.State;
using NGameEditor.Results;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.ProjectWindow;



public interface IInspectorEntityViewModelFactory
{
	InspectorEntityViewModel Create();
}



public class InspectorEntityViewModelFactory(
	SelectedEntitiesState selectedEntitiesState,
	IInspectorComponentViewModelMapper inspectorComponentViewModelMapper,
	IClientRunner<IBackendApi> clientRunner,
	ILogger<InspectorEntityViewModelFactory> logger
) : IInspectorEntityViewModelFactory
{
	public InspectorEntityViewModel Create()
	{
		var inspectorEntityViewModel = new InspectorEntityViewModel();


		selectedEntitiesState
			.SelectedEntities
			.ToObservableChangeSet()
			.ToCollection()
			.Do(x =>
			{
				inspectorEntityViewModel.EntityName = x.Count switch
				{
					0 => "No entity selected",
					1 => x.First().Name,
					_ => "Multiple entities selected"
				};
			})
			.Do(x =>
			{
				inspectorEntityViewModel.CanEditName = x.Count == 1;
			})
			.Do(x =>
			{
				inspectorEntityViewModel.ComponentEditors.Clear();
				if (x.Count != 1) return;

				var entityState = x.First();
				var componentEditors = inspectorComponentViewModelMapper.Map(entityState);
				inspectorEntityViewModel.ComponentEditors.AddRange(componentEditors);
			})
			.Subscribe();

		inspectorEntityViewModel
			.WhenAnyValue(x => x.EntityName)
			.Throttle(TimeSpan.FromMilliseconds(100))
			.ObserveOn(RxApp.MainThreadScheduler)
			.Where(x =>
				string.IsNullOrWhiteSpace(x) == false &&
				selectedEntitiesState.SelectedEntities.Count == 1
			)
			.Select(x => (name: x, entity: selectedEntitiesState.SelectedEntities.First()))
			.Select(x =>
				clientRunner
					.GetClientService()
					.Then(api => api.SetEntityName(x.entity.Id, x.name))
					.IfError(logger.Log)
			)
			.Subscribe();


		return inspectorEntityViewModel;
	}
}
