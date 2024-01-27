using DynamicData.Binding;
using NGameEditor.ViewModels.Components.CustomEditors;

namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews;



public class InspectorViewModel : ViewModelBase
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

	private Action<string>? _updateTitle;

	public Action<string>? UpdateTitle
	{
		get => _updateTitle;
		set => this.RaiseAndSetIfChanged(ref _updateTitle, value);
	}

	private bool _canEditTitle;

	public bool CanEditTitle
	{
		get => _canEditTitle;
		set => this.RaiseAndSetIfChanged(ref _canEditTitle, value);
	}


	public ObservableCollectionExtended<CustomEditorViewModel> CustomEditors { get; } = [];
}
