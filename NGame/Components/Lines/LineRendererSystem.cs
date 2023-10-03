using NGame.Ecs;
using NGame.Renderers;
using NGame.UpdateSchedulers;

namespace NGame.Components.Lines;



public class LineRendererSystem : DataListSystem<LineRendererSystem.Data>, IDrawable
{
	private readonly INGameRenderer _renderer;


	public LineRendererSystem(INGameRenderer renderer)
	{
		_renderer = renderer;
	}


	protected override ICollection<Type> RequiredComponents =>
		new[] { typeof(LineRenderer) };


	protected override Data CreateDataFromEntity(Entity entity)
	{
		var lineRenderer = entity.GetRequiredComponent<LineRenderer>();
		return new Data(lineRenderer);
	}


	void IDrawable.Draw(GameTime gameTime)
	{
		foreach (var data in DataEntries)
		{
			var line = data.LineRenderer.Line;
			if (line == null) continue;
			_renderer.Draw(line);
		}
	}



	public class Data
	{
		public readonly LineRenderer LineRenderer;


		public Data(LineRenderer lineRenderer)
		{
			LineRenderer = lineRenderer;
		}
	}
}
