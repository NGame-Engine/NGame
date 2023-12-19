using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using NGameEditor.ViewModels.Shared;

namespace NGameEditor.Avalonia.Shared;



public class FilePicker : IFilePicker
{
	private readonly IStorageProvider _storageProvider;


	public FilePicker(IStorageProvider storageProvider)
	{
		_storageProvider = storageProvider;
	}


	public async Task<IReadOnlyList<string>> AskUserToPickFile(IFilePicker.OpenOptions openOptions)
	{
		var files =
			await _storageProvider.OpenFilePickerAsync(
				new FilePickerOpenOptions
				{
					Title = openOptions.Title,
					AllowMultiple = openOptions.AllowMultiple,
					FileTypeFilter = MapFileTypeFilters(openOptions.FileTypeFilter),
					SuggestedStartLocation =
						await MapSuggestedStartLocation(openOptions.SuggestedStartLocation)
				}
			);

		return
			files
				.Select(x => Uri.UnescapeDataString(x.Path.AbsolutePath))
				.ToList();
	}


	private static List<FilePickerFileType>? MapFileTypeFilters(IReadOnlyList<IFilePicker.FileType>? fileTypeFilter) =>
		fileTypeFilter?
			.Select(x => new FilePickerFileType(x.Name) { Patterns = x.Patterns })
			.ToList();


	private async Task<IStorageFolder?> MapSuggestedStartLocation(string? suggestedStartLocation) =>
		suggestedStartLocation == null
			? null
			: await _storageProvider.TryGetFolderFromPathAsync(suggestedStartLocation);
}
