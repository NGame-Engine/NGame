using NGameEditor.ViewModels;

namespace NGameEditor.Functionality.Scenes.State;



public class ComponentState(
	Guid id,
	string name,
	bool isRecognized
) : ViewModelBase
{
	public Guid Id => id;
	public string Name => name;
	public bool IsRecognized { get; } = isRecognized;
}
