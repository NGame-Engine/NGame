namespace NGame.Ecs;



public class ComponentTypeEntry
{
	private ComponentTypeEntry(Type subType)
	{
		SubType = subType;
	}


	public static ComponentTypeEntry Create<TComponent>() where TComponent : EntityComponent
		=> new(typeof(TComponent));


	public Type SubType { get; private init; }
}
