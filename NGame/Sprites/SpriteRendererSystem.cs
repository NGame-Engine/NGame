using NGame.Ecs;
using NGame.Renderers;
using NGame.Transforms;
using NGame.UpdateSchedulers;

namespace NGame.Sprites;

public class SpriteRendererSystem : ISystem
{
	private readonly INGameRenderer _renderer;
	private readonly List<Data> _datas = new();


	public SpriteRendererSystem(INGameRenderer renderer)
	{
		_renderer = renderer;
	}


	public IEnumerable<Type> RequiredComponents => new[] { typeof(Transform), typeof(SpriteRenderer) };


	public void Add(Entity entity)
	{
		var transform = entity.GetRequiredComponent<Transform>();
		var spriteRenderer = entity.GetRequiredComponent<SpriteRenderer>();

		var sprite = spriteRenderer.Sprite;

		var rendererSprite = new RendererSprite(sprite.Texture);
		_renderer.Add(rendererSprite);

		var data = new Data(transform, sprite, rendererSprite);
		_datas.Add(data);
	}


	public void Update(GameTime gameTime)
	{
		foreach (var data in _datas)
		{
			data.RendererSprite.SourceRectangle = data.Sprite.SourceRectangle;

			data.RendererSprite.TargetRectangle =
				data.Sprite.TargetRectangle with
				{
					X = data.Sprite.TargetRectangle.X + (int)data.Transform.Position.X,
					Y = data.Sprite.TargetRectangle.Y + (int)data.Transform.Position.Y
				};
		}
	}



	private class Data
	{
		public readonly Transform Transform;
		public readonly Sprite Sprite;
		public readonly RendererSprite RendererSprite;


		public Data(Transform transform, Sprite sprite, RendererSprite rendererSprite)
		{
			RendererSprite = rendererSprite;
			Transform = transform;
			Sprite = sprite;
		}
	}
}
