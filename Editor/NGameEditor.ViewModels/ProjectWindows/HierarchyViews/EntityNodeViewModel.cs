using System.Drawing;
using DynamicData.Binding;
using NGameEditor.ViewModels.Components.Menus;

namespace NGameEditor.ViewModels.ProjectWindows.HierarchyViews;



public class EntityNodeViewModel(
	Guid id,
	string name,
	bool isRecognized
) : ViewModelBase
{
	public Guid Id { get; } = id;
	public bool IsRecognized { get; } = isRecognized;

	public string Name { get; } = name;

	public string DisplayName =>
		IsRecognized
			? Name
			: $" 🚫 {Name}";

	public Color TextColor =>
		IsRecognized
			? Color.White
			: Color.Red;


	public ContextMenuViewModel ContextMenu { get; } = new([]);
	public ObservableCollectionExtended<EntityNodeViewModel> Children { get; set; } = new();
}
