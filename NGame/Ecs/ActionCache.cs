using NGame.Systems;

namespace NGame.Ecs;



public interface IActionCache
{
	void AddAction(Action action);
}



public class ActionCache : IUpdatable, IActionCache
{
	private readonly List<Action> _frameStartActions = new();


	public int Order { get; set; } = -100000;


	public void AddAction(Action action)
	{
		_frameStartActions.Add(action);
	}


	public void Update(GameTime gameTime)
	{
		var actions = _frameStartActions.ToList();


		foreach (var action in actions)
		{
			action();
			_frameStartActions.Remove(action);
		}
	}
}
