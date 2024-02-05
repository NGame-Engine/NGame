using NGameEditor.Bridge.Shared;
using Singulink.IO;

namespace NGameEditor.Functionality.Files;



public static class AbsolutePathExtensions
{
	public static AbsolutePath ToIAbsolutePath(this IAbsolutePath absoluteFilePath) =>
		new(absoluteFilePath.PathDisplay);


	public static IAbsoluteFilePath ToAbsoluteFilePath(this AbsolutePath absolutePath) =>
		FilePath.ParseAbsolute(absolutePath.Path);
}
