using Microsoft.Extensions.DependencyInjection;
using NGame.Scenes;

namespace NGame.Ecs;



public interface IEcsTypeFactory
{
	Entity CreateEntity(Scene scene);
	T CreateComponent<T>() where T : Component;
}



internal class EcsTypeFactory : IEcsTypeFactory
{
	private readonly IServiceProvider _serviceProvider;


	public EcsTypeFactory(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}


	public Entity CreateEntity(Scene scene) =>
		ActivatorUtilities.CreateInstance<Entity>(_serviceProvider, scene);


	public T CreateComponent<T>() where T : Component =>
		_serviceProvider.GetRequiredService<T>();
}
