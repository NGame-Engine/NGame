using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors;
using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors.State;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.ProjectWindow.Inspector;



public interface IObjectSelectorViewModelFactory
{
	ObjectSelectorViewModel Create();
}



public class ObjectSelectorViewModelFactory(
	ObjectSelectionState objectSelectionState,
	IObjectViewModelMapper objectViewModelMapper,
	SelectedObjectViewModel selectedObjectViewModel
) : IObjectSelectorViewModelFactory
{
	public ObjectSelectorViewModel Create()
	{
		var objectSelectorViewModel = new ObjectSelectorViewModel(selectedObjectViewModel);


		var observablePredicate =
			objectSelectorViewModel
				.WhenAnyValue(viewModel => viewModel.SearchFilter)
				.Select<string, Func<SelectableObjectState, bool>>(searchFilter =>
					entity => IsMatchingFilter(entity, searchFilter)
				);

		objectSelectionState
			.AvailableObjects
			.ToObservableChangeSet()
			.Filter(observablePredicate)
			.Transform(objectViewModelMapper.Map)
			.Bind(objectSelectorViewModel.FilteredObjects)
			.Subscribe();


		objectSelectorViewModel
			.SelectedObjects
			.ToObservableChangeSet()
			.Transform(x =>
				objectSelectionState
					.AvailableObjects
					.First(state => state.Id == x.Id)
			)
			.Bind(objectSelectionState.SelectedObjects)
			.Subscribe();


		return objectSelectorViewModel;
	}


	private static bool IsMatchingFilter(SelectableObjectState selectableObjectState, string searchFilter) =>
		string.IsNullOrWhiteSpace(searchFilter) ||
		selectableObjectState.FullName.Contains(searchFilter, StringComparison.OrdinalIgnoreCase);
}
