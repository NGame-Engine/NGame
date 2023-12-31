using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using NGameEditor.Functionality.Windows.ProjectWindow;

namespace NGameEditor.Avalonia.Shared;



public interface IFilePickerOpener
{
	Task<IReadOnlyList<string>> OpenFilePicker(
		IStorageProvider storageProvider,
		OpenFileOptions openFileOptions
	);


	Task<IReadOnlyList<string>> OpenFolderPicker(
		IStorageProvider storageProvider,
		OpenFolderOptions openFolderOptions
	);
}



public class FilePickerOpener : IFilePickerOpener
{
	public async Task<IReadOnlyList<string>> OpenFilePicker(
		IStorageProvider storageProvider,
		OpenFileOptions openFileOptions
	) =>
		await storageProvider
			.OpenFilePickerAsync(
				new FilePickerOpenOptions
				{
					Title = openFileOptions.Title,
					AllowMultiple = openFileOptions.AllowMultiple,
					FileTypeFilter = openFileOptions.FileTypeFilter?
						.Select(x => new FilePickerFileType(x.Name) { Patterns = x.Patterns })
						.ToList(),
					SuggestedStartLocation =
						openFileOptions.SuggestedStartLocation == null
							? null
							: await storageProvider.TryGetFolderFromPathAsync(openFileOptions.SuggestedStartLocation)
				}
			)
			.ContinueWith(x =>
				x
					.Result
					.Select(file => Uri.UnescapeDataString(file.Path.AbsolutePath))
					.ToList()
			);


	public async Task<IReadOnlyList<string>> OpenFolderPicker(
		IStorageProvider storageProvider,
		OpenFolderOptions openFolderOptions
	) =>
		await storageProvider.OpenFolderPickerAsync(
				new FolderPickerOpenOptions
				{
					Title = openFolderOptions.Title,
					AllowMultiple = openFolderOptions.AllowMultiple,
					SuggestedStartLocation =
						openFolderOptions.SuggestedStartLocation == null
							? null
							: await storageProvider.TryGetFolderFromPathAsync(openFolderOptions.SuggestedStartLocation)
				}
			)
			.ContinueWith(x =>
				x
					.Result
					.Select(folder => Uri.UnescapeDataString(folder.Path.AbsolutePath))
					.ToList()
			);
}
