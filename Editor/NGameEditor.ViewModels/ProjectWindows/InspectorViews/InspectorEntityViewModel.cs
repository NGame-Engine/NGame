using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using NGameEditor.ViewModels.Components.CustomEditors;
using NGameEditor.ViewModels.Controllers;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews.Components;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews;



public class InspectorEntityViewModel : ViewModelBase
{
	public InspectorEntityViewModel(
		SelectedEntitiesState selectedEntitiesState,
		IEntityController entityController,
		IInspectorComponentViewModelMapper inspectorComponentViewModelMapper
	)
	{
		/*
		selectedEntitiesState
			.SelectedEntities
			.ToObservableChangeSet()
			.TransformMany(x => 
				x
					.Components
					.Select(state => inspectorComponentViewModelMapper.Map(state, x))
				)
			.Bind(Components)
			.Subscribe();
			*/

		selectedEntitiesState
			.SelectedEntities
			.ToObservableChangeSet()
			.ToCollection()
			.Select(x =>
			{
				EntityName = x.Count switch
				{
					0 => "No entity selected",
					1 => x.First().Name,
					_ => "Multiple entities selected"
				};

				return x;
			})
			.Select(x =>
			{
				CanEditName = x.Count == 1;
				return x;
			})
			.Select(x =>
			{
				ComponentEditors.Clear();
				if (x.Count != 1) return x;

				var entityState = x.First();
				var componentEditors = inspectorComponentViewModelMapper.Map(entityState);
				ComponentEditors.AddRange(componentEditors);
				
				return x;
			})
			.Subscribe();

		this.WhenAnyValue(x => x.EntityName)
			.Throttle(TimeSpan.FromMilliseconds(100))
			.ObserveOn(RxApp.MainThreadScheduler)
			.Where(x =>
				string.IsNullOrWhiteSpace(x) == false &&
				selectedEntitiesState.SelectedEntities.Count == 1
			)
			.Select(x => (name: x, entity: selectedEntitiesState.SelectedEntities.First()))
			.Select(x => entityController.SetName(x.entity, x.name))
			.Subscribe();
	}


	private bool _canEditName;

	public bool CanEditName
	{
		get => _canEditName;
		set => this.RaiseAndSetIfChanged(ref _canEditName, value);
	}

	private string _entityName = "";

	public string EntityName
	{
		get => _entityName;
		set => this.RaiseAndSetIfChanged(ref _entityName, value);
	}

	public ObservableCollectionExtended<ComponentEditorViewModel> ComponentEditors { get; } = new();
	public ObservableCollectionExtended<InspectorComponentViewModel> Components { get; } = new();
}
