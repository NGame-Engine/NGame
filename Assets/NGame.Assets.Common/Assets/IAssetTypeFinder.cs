using System.Reflection;

namespace NGame.Assets.Common.Assets;



public interface IAssetTypeFinder
{
	public ISet<Type> FindAssetTypes(Assembly assembly);
	public ISet<Type> FindComponentTypes(Assembly assembly);
}
