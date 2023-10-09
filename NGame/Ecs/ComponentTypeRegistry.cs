using Microsoft.Extensions.Logging;
using NGame.Services;

namespace NGame.Ecs;



public class ComponentTypeRegistry : IComponentTypeRegistry
{
	private readonly ILogger<ComponentTypeRegistry> _logger;
	private readonly ISet<Type> _registeredTypes = new HashSet<Type>();


	public ComponentTypeRegistry(ILogger<ComponentTypeRegistry> logger)
	{
		_logger = logger;
	}


	void IComponentTypeRegistry.Register(Type type)
	{
		if (!_registeredTypes.Add(type))
		{
			var fullName = type.FullName;
			_logger.LogError("Component {FullName} is already registered", fullName);
			throw new InvalidOperationException($"Component {fullName} is already registered");
		}
	}


	void IComponentTypeRegistry.Register<T>()
	{
		var type = typeof(T);
		if (!_registeredTypes.Add(type))
		{
			var fullName = type.FullName;
			_logger.LogError("Component {FullName} is already registered", fullName);
			throw new InvalidOperationException($"Component {fullName} is already registered");
		}
	}


	ICollection<Type> IComponentTypeRegistry.GetComponentTypes() => _registeredTypes.ToList();
}
