using DynamicData.Binding;
using NGameEditor.ViewModels.Components.CustomEditors;

namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews;



public class InspectorEntityViewModel : ViewModelBase
{
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

	public ObservableCollectionExtended<CustomEditorViewModel> ComponentEditors { get; } = new();
}
