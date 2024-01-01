using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Functionality.Scenes;
using NGameEditor.Functionality.Scenes.State;
using NGameEditor.Results;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.ProjectWindow;



public interface IInspectorViewModelFactory
{
	InspectorViewModel Create();
}



public class InspectorViewModelFactory(
	SelectedEntitiesState selectedEntitiesState,
	IInspectorComponentViewModelMapper inspectorComponentViewModelMapper,
	IClientRunner<IBackendApi> clientRunner,
	ILogger<InspectorViewModelFactory> logger,
	DirectoryContentViewModel directoryContentViewModel
) : IInspectorViewModelFactory
{
	public InspectorViewModel Create()
	{
		var inspectorViewModel = new InspectorViewModel();

		selectedEntitiesState
			.SelectedEntities
			.ToObservableChangeSet()
			.ToCollection()
			.Do(x => SetEntity(inspectorViewModel, x))
			.Subscribe();

		directoryContentViewModel
			.SelectedItems
			.ToObservableChangeSet()
			.ToCollection()
			.Where(x =>
				x.Any(i => i.IsFolder == false)
			)
			.Do(x => SetDirectoryContentItem(inspectorViewModel, x))
			.Subscribe();


		inspectorViewModel
			.WhenAnyValue(x => x.UpdateTitle)
			.Do(x => inspectorViewModel.CanEditTitle = x != null)
			.Subscribe();

		inspectorViewModel
			.WhenAnyValue(x => x.Title)
			.Throttle(TimeSpan.FromMilliseconds(100))
			.Where(_ => inspectorViewModel.CanEditTitle)
			.ObserveOn(RxApp.MainThreadScheduler)
			.Do(x => inspectorViewModel.UpdateTitle?.Invoke(x))
			.Subscribe();


		return inspectorViewModel;
	}


	private void SetEntity(
		InspectorViewModel inspectorViewModel,
		IReadOnlyCollection<EntityState> selectedEntities
	)
	{
		inspectorViewModel.Icon = "♟️";

		inspectorViewModel.UpdateTitle = null;
		inspectorViewModel.CustomEditors.Clear();

		if (selectedEntities.Count == 0)
		{
			inspectorViewModel.Title = "No entity selected";
			return;
		}

		if (selectedEntities.Count > 1)
		{
			inspectorViewModel.Title = "Multiple entities selected";
			return;
		}


		var entity = selectedEntities.First();

		inspectorViewModel.Title = entity.Name;

		inspectorViewModel.UpdateTitle = x =>
			clientRunner
				.GetClientService()
				.Then(api => api.SetEntityName(entity.Id, x))
				.IfError(logger.Log);

		var componentEditors = inspectorComponentViewModelMapper.Map(entity);
		inspectorViewModel.CustomEditors.AddRange(componentEditors);
	}


	private void SetDirectoryContentItem(
		InspectorViewModel inspectorViewModel,
		IReadOnlyCollection<DirectoryContentItemViewModel> selectedContentItems
	)
	{
		inspectorViewModel.Icon = "📄";

		inspectorViewModel.UpdateTitle = null;
		inspectorViewModel.CustomEditors.Clear();

		if (selectedContentItems.Count > 1)
		{
			inspectorViewModel.Title = "Multiple files selected";
			return;
		}


		var file = selectedContentItems.First();

		inspectorViewModel.Title = file.DisplayName;
	}
}
