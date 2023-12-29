using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using NGameEditor.ViewModels.Shared;

namespace NGameEditor.Avalonia.Shared;



public class FolderPicker : IFolderPicker
{
	private readonly IStorageProvider _storageProvider;


	public FolderPicker(IStorageProvider storageProvider)
	{
		_storageProvider = storageProvider;
	}


	public async Task<IReadOnlyList<string>> AskUserToPickFolder(IFolderPicker.OpenOptions openOptions)
	{
		var files =
			await _storageProvider.OpenFolderPickerAsync(
				new FolderPickerOpenOptions
				{
					Title = openOptions.Title,
					AllowMultiple = openOptions.AllowMultiple,
					SuggestedStartLocation =
						await MapSuggestedStartLocation(openOptions.SuggestedStartLocation)
				}
			);

		return
			files
				.Select(x => Uri.UnescapeDataString(x.Path.AbsolutePath))
				.ToList();
	}


	private async Task<IStorageFolder?> MapSuggestedStartLocation(string? suggestedStartLocation) =>
		suggestedStartLocation == null
			? null
			: await _storageProvider.TryGetFolderFromPathAsync(suggestedStartLocation);
}
