using NGameEditor.Bridge.Shared;
using Singulink.IO;

namespace NGameEditor.Backend.Files;



public static class AbsolutePathExtensions
{
	public static AbsolutePath FromAbsoluteFilePath(this IAbsoluteFilePath absoluteFilePath) =>
		new(absoluteFilePath.PathDisplay);


	public static IAbsoluteFilePath ToAbsoluteFilePath(this AbsolutePath absolutePath) =>
		FilePath.ParseAbsolute(absolutePath.Path);
}
