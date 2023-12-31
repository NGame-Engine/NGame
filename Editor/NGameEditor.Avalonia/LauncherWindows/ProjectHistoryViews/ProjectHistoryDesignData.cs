using System;
using NGameEditor.ViewModels.LauncherWindows.HistoryViews;

namespace NGameEditor.Avalonia.LauncherWindows.ProjectHistoryViews;



public static class ProjectHistoryDesignData
{
	public static ProjectHistoryViewModel ProjectHistoryExample { get; } =
		new()
		{
			ProjectUsages =
			{
				new HistoryEntryViewModel(
					"/mypath/FirstProject.sln",
					DateTime.UnixEpoch,
					ReactiveCommand.Create(() => { })
				),

				new HistoryEntryViewModel(
					"/another path/SecondProject.sln",
					DateTime.UnixEpoch,
					ReactiveCommand.Create(() => { })
				),

				new HistoryEntryViewModel(
					"/a third parth/ThirdProject.sln",
					DateTime.UnixEpoch,
					ReactiveCommand.Create(() => { })
				),
			}
		};
}
