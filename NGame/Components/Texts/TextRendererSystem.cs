using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.Renderers;
using NGame.UpdateSchedulers;

namespace NGame.Components.Texts;



internal class TextRendererSystem : DataListSystem<TextRendererSystem.Data>, IDrawable
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
		var spriteRenderer = entity.GetRequiredComponent<TextRenderer>();

		var sprite = spriteRenderer.Text;

		return new Data(transform, sprite);
	}


	void IDrawable.Draw(GameTime gameTime)
	{
		foreach (var data in DataEntries)
		{
			if (data.Text == null) continue;

			_renderer.Draw(data.Text, data.Transform);
		}
	}



	internal class Data
	{
		public readonly Transform Transform;
		public readonly Text? Text;


		public Data(Transform transform, Text? text)
		{
			Transform = transform;
			Text = text;
		}
	}
}
