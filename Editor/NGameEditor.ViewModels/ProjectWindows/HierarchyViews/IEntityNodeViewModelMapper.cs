using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.HierarchyViews;



public interface IEntityNodeViewModelMapper
{
	EntityNodeViewModel Map(EntityState entityState);
}
