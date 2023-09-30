using System.Numerics;
using NGame.Ecs;
using NGame.Renderers;
using NGame.Texts;
using NGame.Transforms;
using NGame.UpdateSchedulers;

namespace NGame.Sprites;

public class SpriteRendererSystem : ISystem, IDrawable
{
	private readonly INGameRenderer _renderer;
	private readonly List<Data> _datas = new();


	public SpriteRendererSystem(INGameRenderer renderer)
	{
		_renderer = renderer;
	}


	IEnumerable<Type> ISystem.RequiredComponents => new[] { typeof(Transform), typeof(SpriteRenderer) };


	void ISystem.Add(Entity entity)
	{
		var transform = entity.GetRequiredComponent<Transform>();
		var spriteRenderer = entity.GetRequiredComponent<SpriteRenderer>();

		var sprite = spriteRenderer.Sprite;

		var data = new Data(transform, sprite);
		_datas.Add(data);
	}


	private Font font = new Font
	{
		FilePath = "Fonts/YanoneKaffeesatz-VariableFont_wght.ttf"
	};


	void IDrawable.Draw(GameTime gameTime)
	{
		foreach (var data in _datas)
		{
			if (data.Sprite == null) continue;

			_renderer.Draw(data.Sprite, data.Transform);
		}

		_renderer.Draw(new Line
		{
			Vertices = new List<Vector2>
			{
				new Vector2(1, 2),
				new Vector2(40, 50),
				new Vector2(60, 30)
			}
		});
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
