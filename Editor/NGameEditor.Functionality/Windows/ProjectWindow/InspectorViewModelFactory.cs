using NGameEditor.ViewModels.ProjectWindows.InspectorViews;

namespace NGameEditor.Functionality.Windows.ProjectWindow;



public interface IInspectorViewModelFactory
{
	InspectorViewModel Create();
}



public class InspectorViewModelFactory(
	InspectorEntityViewModel inspectorEntityViewModel
) : IInspectorViewModelFactory
{
	public InspectorViewModel Create()
	{
		var inspectorViewModel = new InspectorViewModel(inspectorEntityViewModel);


		return inspectorViewModel;
	}
}
