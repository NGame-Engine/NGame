using NGame.Ecs;

namespace NGame.Core.SceneAssets;

public class ComponentTypeEntry
{
	public ComponentTypeEntry(Type subType)
	{
		if (subType.IsAssignableTo(typeof(EntityComponent)) == false)
		{
			throw new InvalidOperationException($"{subType} is not a sub type of {nameof(EntityComponent)}");
		}

		SubType = subType;
	}


	public Type SubType { get; private init; }
}
