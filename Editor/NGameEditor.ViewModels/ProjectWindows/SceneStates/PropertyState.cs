namespace NGameEditor.ViewModels.ProjectWindows.SceneStates;



public class PropertyState(
	string name,
	string typeIdentifier,
	string valueObject
) : ViewModelBase
{
	public string Name
	{
		get => name;
		set => this.RaiseAndSetIfChanged(ref name, value);
	}

	public string TypeIdentifier { get; } = typeIdentifier;

	public string Value
	{
		get => valueObject;
		set => this.RaiseAndSetIfChanged(ref valueObject, value);
	}
}
