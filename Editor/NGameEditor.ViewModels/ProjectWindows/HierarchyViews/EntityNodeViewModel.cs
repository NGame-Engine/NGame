using System.Drawing;
using DynamicData.Binding;
using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.HierarchyViews;



public class EntityNodeViewModel(
	EntityState entityState,
	ContextMenuViewModel contextMenu
) : ViewModelBase
{
	public EntityState EntityState { get; } = entityState;

	public string Name =>
		EntityState.Components.All(x => x.IsRecognized)
			? EntityState.Name
			: $" 🚫 {EntityState.Name}";

	public Color TextColor =>
		EntityState.Components.All(x => x.IsRecognized)
			? Color.White
			: Color.Red;


	public ContextMenuViewModel ContextMenu { get; } = contextMenu;
	public ObservableCollectionExtended<EntityNodeViewModel> Children { get; set; } = new();
}
