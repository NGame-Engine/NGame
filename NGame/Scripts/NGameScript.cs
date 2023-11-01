using NGame.Components;
using NGame.Ecs;

namespace NGame.Scripts;



public abstract class NGameScript : Component
{
	private readonly IComponentFactory _componentFactory;


	protected NGameScript(IComponentFactory componentFactory)
	{
		_componentFactory = componentFactory;
	}


	public T AddComponent<T>() where T : Component =>
		_componentFactory.CreateComponent<T>(Entity!);


	public T GetComponent<T>() where T : Component =>
		Entity!.GetComponent<T>();


	public T? TryGetComponent<T>() where T : Component =>
		Entity!.TryGetComponent<T>();


	public IEnumerable<T> GetComponents<T>() where T : Component =>
		Entity!.GetComponents<T>();


	public IEnumerable<T> GetComponentsInChildren<T>() where T : Component =>
		Entity!.GetComponentsInChildren<T>();
}
