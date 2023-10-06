using NGame.Components;

namespace NGame.Services.Scripts;



public class NGameScript : Component
{
	private readonly IComponentFactory _componentFactory;


	public NGameScript(IComponentFactory componentFactory)
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


	public Entity Instantiate(Entity entity) =>
		Entity!.Scene.Instantiate(entity);
}
