using System.Reflection;
using NGame.Assets;
using NGame.Services;

namespace NGame.Resources;



public class ResourceRegistry<T> : IResourceRegistry<T> where T : Asset
{
	private readonly Dictionary<T, IResourceRegistry<T>.Entry> _assets = new();


	public void Register(T asset, Assembly assembly)
	{
		var assemblyName = assembly.GetName().Name!;

		var pathParts =
			asset
				.FilePath
				.Split(
					new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar },
					StringSplitOptions.RemoveEmptyEntries
				)
				.Prepend(assemblyName);

		var resourcePath = string.Join('.', pathParts);

		if (!assembly.GetManifestResourceNames().Contains(resourcePath))
		{
			throw new InvalidOperationException($"Embedded resource {resourcePath} does not exist");
		}

		var entry = new IResourceRegistry<T>.Entry(assembly, resourcePath);

		_assets.Add(asset, entry);
	}


	public IEnumerable<IResourceRegistry<T>.Entry> GetAll() => _assets.Values;


	public IResourceRegistry<T>.Entry GetEntry(T asset) => _assets[asset];


	public Stream GetStream(T asset)
	{
		var entry = _assets[asset];
		var assembly = entry.Assembly;
		var resourcePath = entry.ResourceFileName;
		return assembly.GetManifestResourceStream(resourcePath)!;
	}
}
