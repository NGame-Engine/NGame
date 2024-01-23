using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors;
using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors.State;

namespace NGameEditor.Functionality.Windows.ProjectWindow.Inspector;



public interface ISelectedObjectViewModelFactory
{
	SelectedObjectViewModel Create();
}



public class SelectedObjectViewModelFactory(
	ObjectSelectionState objectSelectionState
) : ISelectedObjectViewModelFactory
{
	public SelectedObjectViewModel Create()
	{
		var selectedObjectViewModel = new SelectedObjectViewModel();


		objectSelectionState
			.SelectedObjects
			.ToObservableChangeSet()
			.ToCollection()
			.Do(objects =>
				{
					if (objects.Count == 0)
					{
						selectedObjectViewModel.Name = "Nothing selected";
						selectedObjectViewModel.KindName = "";
						selectedObjectViewModel.Path = "";
						return;
					}

					if (objects.Count > 1)
					{
						throw new NotImplementedException();
					}

					var objectViewModel = objects.First();
					selectedObjectViewModel.Name = objectViewModel.Name;
					selectedObjectViewModel.KindName = objectViewModel.KindName;
					selectedObjectViewModel.Path = objectViewModel.Path;
				}
			)
			.Subscribe();


		return selectedObjectViewModel;
	}
}
