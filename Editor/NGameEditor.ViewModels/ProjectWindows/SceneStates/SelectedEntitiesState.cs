using DynamicData.Binding;

namespace NGameEditor.ViewModels.ProjectWindows.SceneStates;



public class SelectedEntitiesState : ViewModelBase
{
	public ObservableCollectionExtended<EntityState> SelectedEntities { get; } = new();
}
