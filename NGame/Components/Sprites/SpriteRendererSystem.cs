using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.Renderers;
using NGame.UpdateSchedulers;

namespace NGame.Components.Sprites;



internal class SpriteRendererSystem : ISystem, IDrawable
{
	private readonly INGameRenderer _renderer;
	private readonly List<Data> _datas = new();


	public SpriteRendererSystem(INGameRenderer renderer)
	{
		_renderer = renderer;
	}


	ICollection<Type> ISystem.RequiredComponents => new[] { typeof(Transform), typeof(SpriteRenderer) };


	void ISystem.Add(Entity entity)
	{
		var transform = entity.GetRequiredComponent<Transform>();
		var spriteRenderer = entity.GetRequiredComponent<SpriteRenderer>();

		var sprite = spriteRenderer.Sprite;

		var data = new Data(transform, sprite);
		_datas.Add(data);
	}


	void IDrawable.Draw(GameTime gameTime)
	{
		foreach (var data in _datas)
		{
			if (data.Sprite == null) continue;

			_renderer.Draw(data.Sprite, data.Transform);
		}
	}



	private class Data
	{
		public readonly Transform Transform;
		public readonly Sprite? Sprite;


		public Data(Transform transform, Sprite? sprite)
		{
			Transform = transform;
			Sprite = sprite;
		}
	}
}
