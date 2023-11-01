namespace NGame.Services;



public class TypeRegistryEntry<T>
{
	public TypeRegistryEntry(IReadOnlyCollection<Type> types)
	{
		Types = types;
	}


	public IReadOnlyCollection<Type> Types { get; }
}



public interface ITypeRegistry
{
	void AddType<TCategory, TEntry>() where TEntry : TCategory;
	TypeRegistryEntry<T> GetEntry<T>();
}
