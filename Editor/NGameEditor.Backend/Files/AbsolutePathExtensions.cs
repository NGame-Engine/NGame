using NGameEditor.Bridge.Shared;
using Singulink.IO;

namespace NGameEditor.Backend.Files;



public static class AbsolutePathExtensions
{
	public static CompatibleAbsolutePath FromAbsoluteFilePath(this IAbsoluteFilePath absoluteFilePath) =>
		new(absoluteFilePath.PathDisplay);


	public static IAbsoluteFilePath ToAbsoluteFilePath(this CompatibleAbsolutePath compatibleAbsolutePath) =>
		FilePath.ParseAbsolute(compatibleAbsolutePath.Path);
}
