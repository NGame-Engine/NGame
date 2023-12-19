using NGameEditor.Results;
using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.Controllers;



public interface IEntityController
{
	IEnumerable<MenuEntryViewModel> GetAddComponentMenuEntries(EntityState entityState);
	Result SetName(EntityState entity, string newName);
}
