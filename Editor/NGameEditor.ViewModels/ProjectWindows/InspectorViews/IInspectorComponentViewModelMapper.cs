using NGameEditor.ViewModels.Components.CustomEditors;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews;



public interface IInspectorComponentViewModelMapper
{
	IEnumerable<ComponentEditorViewModel> Map(EntityState entityState);
}
