using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.Renderers;
using NGame.UpdateSchedulers;

namespace NGame.Components.Lines;



internal class LineRendererSystem : ISystem, IDrawable
{
	private readonly INGameRenderer _renderer;
	private readonly List<Data> _datas = new();


	public LineRendererSystem(INGameRenderer renderer)
	{
		_renderer = renderer;
	}


	IEnumerable<Type> ISystem.RequiredComponents => new[] { typeof(Transform), typeof(LineRenderer) };


	void ISystem.Add(Entity entity)
	{
		var lineRenderer = entity.GetRequiredComponent<LineRenderer>();

		var line = lineRenderer.Line;
		if (line == null) return;

		var data = new Data(line);
		_datas.Add(data);
	}


	void IDrawable.Draw(GameTime gameTime)
	{
		foreach (var data in _datas)
		{
			_renderer.Draw(data.Line);
		}
	}



	private class Data
	{
		public readonly Line Line;


		public Data(Line line)
		{
			Line = line;
		}
	}
}
