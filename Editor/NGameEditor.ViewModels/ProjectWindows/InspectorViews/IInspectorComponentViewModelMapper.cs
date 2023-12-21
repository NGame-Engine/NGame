using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews;



public interface IInspectorComponentViewModelMapper
{
	InspectorComponentViewModel Map(ComponentState componentState, EntityState entityState);
}
