using NGame.Ecs;
using NGame.Renderers;
using NGame.UpdateSchedulers;

namespace NGame.Components.Lines;



internal class LineRendererSystem : DataListSystem<LineRendererSystem.Data>, IDrawable
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
		var line = lineRenderer.Line;
		return new Data(line);
	}


	void IDrawable.Draw(GameTime gameTime)
	{
		foreach (var data in DataEntries)
		{
			if (data.Line == null) continue;
			_renderer.Draw(data.Line);
		}
	}



	internal class Data
	{
		public readonly Line? Line;


		public Data(Line? line)
		{
			Line = line;
		}
	}
}
