using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors;
using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors.State;

namespace NGameEditor.Functionality.Windows.ProjectWindow;



public interface IObjectViewModelMapper
{
	ObjectViewModel Map(SelectableObjectState selectableObjectState);
}



public class ObjectViewModelMapper : IObjectViewModelMapper
{
	public ObjectViewModel Map(SelectableObjectState selectableObjectState) =>
		new(
			selectableObjectState.Id,
			"❔",
			selectableObjectState.FullName
		);
}
