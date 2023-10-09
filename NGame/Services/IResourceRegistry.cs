using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NGame.Assets;
using NGame.Setup;

namespace NGame.Services;



public static class ResourceRegistryExtensions
{
	public static INGame RegisterAsset<T>(
		this INGame app,
		T asset
	) where T : Asset
	{
		var resourceRegistry = app.Services.GetRequiredService<IResourceRegistry<T>>();
		resourceRegistry.Register(asset, Assembly.GetCallingAssembly());
		return app;
	}
}



public interface IResourceRegistry<T> where T : Asset
{
	void Register(T asset, Assembly assembly);
	IEnumerable<KeyValuePair<T, Entry>> GetAll();
	Entry GetEntry(T asset);
	Stream GetStream(T asset);



	public class Entry
	{
		public Entry(Assembly assembly, string resourceFileName)
		{
			Assembly = assembly;
			ResourceFileName = resourceFileName;
		}


		public Assembly Assembly { get; }
		public string ResourceFileName { get; }
	}
}
