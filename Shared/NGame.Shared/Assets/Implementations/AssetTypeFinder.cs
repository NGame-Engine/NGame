using System.Reflection;
using NGame.Ecs;

namespace NGame.Assets.Implementations;

public class AssetTypeFinder : IAssetTypeFinder
{
	public ISet<Type> FindAssetTypes(Assembly assembly) =>
		GetTypesRecursive(
				assembly,
				x =>
					x.IsAbstract == false &&
					x.IsAssignableTo(typeof(Asset)) &&
					x != typeof(Asset)
			)
			.Distinct()
			.ToHashSet();


	public ISet<Type> FindComponentTypes(Assembly assembly) =>
		GetTypesRecursive(
				assembly,
				x =>
					x.IsAbstract == false &&
					x.IsAssignableTo(typeof(EntityComponent)) &&
					x != typeof(EntityComponent)
			)
			.Distinct()
			.ToHashSet();


	private IEnumerable<Type> GetTypesRecursive(Assembly assembly, Func<Type, bool> predicate)
	{
		return GetTypesRecursive(assembly, new HashSet<Assembly>(), predicate);
	}


	private IEnumerable<Type> GetTypesRecursive(
		Assembly assembly,
		HashSet<Assembly> searchedAssemblies,
		Func<Type, bool> predicate
	)
	{
		if (
			searchedAssemblies.Add(assembly) == false ||
			assembly.FullName!.StartsWith("System.") ||
			assembly.FullName!.StartsWith("Microsoft."))
		{
			yield break;
		}

		var childTypes =
			assembly
				.GetReferencedAssemblies()
				.SelectMany(x => GetTypesRecursive(Assembly.Load(x), searchedAssemblies, predicate));

		foreach (var childType in childTypes)
		{
			yield return childType;
		}


		var types =
			assembly
				.ExportedTypes
				.Where(predicate);

		foreach (var type in types)
		{
			yield return type;
		}
	}
}
