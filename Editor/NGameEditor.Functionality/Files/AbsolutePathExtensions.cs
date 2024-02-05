using NGameEditor.Bridge.Shared;
using Singulink.IO;

namespace NGameEditor.Functionality.Files;



public static class AbsolutePathExtensions
{
	public static CompatibleAbsolutePath ToCompatiblePath(this IAbsolutePath absoluteFilePath) =>
		new(absoluteFilePath.PathDisplay);
}
