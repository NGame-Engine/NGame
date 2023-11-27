using System.Reflection;

namespace NGame.Assets;



public interface IAssetTypeFinder
{
	public ISet<Type> FindAssetSubTypes(Assembly assembly);
	public ISet<Type> FindEntityComponentSubTypes(Assembly assembly);
}
