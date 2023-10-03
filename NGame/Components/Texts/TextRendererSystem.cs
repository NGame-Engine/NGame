using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.Renderers;
using NGame.UpdateSchedulers;

namespace NGame.Components.Texts;



public class TextRendererSystem : DataListSystem<TextRendererSystem.Data>, IDrawable
{
	private readonly INGameRenderer _renderer;


	public TextRendererSystem(INGameRenderer renderer)
	{
		_renderer = renderer;
	}


	protected override ICollection<Type> RequiredComponents =>
		new[] { typeof(TextRenderer) };


	protected override Data CreateDataFromEntity(Entity entity)
	{
		var transform = entity.Transform;
		var textRenderer = entity.GetRequiredComponent<TextRenderer>();
		return new Data(transform, textRenderer);
	}


	void IDrawable.Draw(GameTime gameTime)
	{
		foreach (var data in DataEntries)
		{
			var text = data.TextRenderer.Text;
			if (text == null) continue;

			_renderer.Draw(text, data.Transform);
		}
	}



	public class Data
	{
		public readonly Transform Transform;
		public readonly TextRenderer TextRenderer;


		public Data(Transform transform, TextRenderer textRenderer)
		{
			Transform = transform;
			TextRenderer = textRenderer;
		}
	}
}
