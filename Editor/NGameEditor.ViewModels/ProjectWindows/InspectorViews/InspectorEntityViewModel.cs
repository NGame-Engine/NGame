using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using NGameEditor.ViewModels.Controllers;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews;



public class InspectorEntityViewModel : ViewModelBase
{
	public InspectorEntityViewModel(
		SelectedEntitiesState selectedEntitiesState,
		IEntityController entityController
	)
	{
		selectedEntitiesState
			.SelectedEntities
			.ToObservableChangeSet()
			.TransformMany(x => x.Components)
			.Transform(Map)
			.Bind(Components)
			.Subscribe();

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

	public ObservableCollectionExtended<InspectorComponentViewModel> Components { get; } = new();


	private static InspectorComponentViewModel Map(ComponentState componentState)
	{
		return new InspectorComponentViewModel(componentState, componentState.Properties);
	}
}
