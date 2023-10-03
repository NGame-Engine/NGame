using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.Renderers;
using NGame.UpdateSchedulers;

namespace NGame.Components.Sprites;



public class SpriteRendererSystem : DataListSystem<SpriteRendererSystem.Data>, IDrawable
{
	private readonly INGameRenderer _renderer;


	public SpriteRendererSystem(INGameRenderer renderer)
	{
		_renderer = renderer;
	}


	protected override ICollection<Type> RequiredComponents =>
		new[] { typeof(SpriteRenderer) };


	protected override Data CreateDataFromEntity(Entity entity)
	{
		var transform = entity.Transform;
		var spriteRenderer = entity.GetRequiredComponent<SpriteRenderer>();

		return new Data(transform, spriteRenderer);
	}


	void IDrawable.Draw(GameTime gameTime)
	{
		foreach (var data in DataEntries)
		{
			var sprite = data.SpriteRenderer.Sprite;
			if (sprite == null) continue;

			_renderer.Draw(sprite, data.Transform);
		}
	}



	public class Data
	{
		public readonly Transform Transform;
		public readonly SpriteRenderer SpriteRenderer;


		public Data(Transform transform, SpriteRenderer spriteRenderer)
		{
			Transform = transform;
			SpriteRenderer = spriteRenderer;
		}
	}
}
