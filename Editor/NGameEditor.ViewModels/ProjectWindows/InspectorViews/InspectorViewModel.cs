using DynamicData.Binding;
using NGameEditor.ViewModels.Components.CustomEditors;

namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews;



public class InspectorViewModel(
	InspectorEntityViewModel entity
) : ViewModelBase
{
	private string _icon = "❔";

	public string Icon
	{
		get => _icon;
		set => this.RaiseAndSetIfChanged(ref _icon, value);
	}

	private string _title = "";

	public string Title
	{
		get => _title;
		set => this.RaiseAndSetIfChanged(ref _title, value);
	}

	private bool _canEditTitle;

	public bool CanEditTitle
	{
		get => _canEditTitle;
		set => this.RaiseAndSetIfChanged(ref _canEditTitle, value);
	}

	public ObservableCollectionExtended<CustomEditorViewModel> CustomEditors { get; } = new();


	[Obsolete]
	public InspectorEntityViewModel Entity
	{
		get => entity;
		set => this.RaiseAndSetIfChanged(ref entity, value);
	}
}
