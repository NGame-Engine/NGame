using System.Drawing;
using DynamicData;
using DynamicData.Binding;
using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.HierarchyViews;



public class EntityNodeViewModel : ViewModelBase
{
	public EntityNodeViewModel(
		EntityState entityState,
		ContextMenuViewModel contextMenu,
		Func<EntityState, EntityNodeViewModel> map
	)
	{
		EntityState = entityState;
		ContextMenu = contextMenu;

		entityState
			.Children
			.ToObservableChangeSet()
			.Transform(map)
			.Bind(Children)
			.Subscribe();
	}


	public EntityState EntityState { get; }

	public string Name =>
		EntityState.Components.All(x => x.IsRecognized)
			? EntityState.Name
			: $" 🚫 {EntityState.Name}";

	public Color TextColor =>
		EntityState.Components.All(x => x.IsRecognized)
			? Color.White
			: Color.Red;


	public ContextMenuViewModel ContextMenu { get; }
	public ObservableCollectionExtended<EntityNodeViewModel> Children { get; set; } = new();
}
