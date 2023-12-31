using DynamicData.Binding;
using NGameEditor.ViewModels;

namespace NGameEditor.Functionality.Scenes.State;



public class SelectedEntitiesState : ViewModelBase
{
	public ObservableCollectionExtended<EntityState> SelectedEntities { get; } = new();
}
