﻿using NGame.Ecs;
using NGame.Renderers;
using NGame.Transforms;
using NGame.UpdateSchedulers;

namespace NGame.Texts;

public class TextRendererSystem : ISystem, IDrawable
{
	private readonly INGameRenderer _renderer;
	private readonly List<Data> _datas = new();


	public TextRendererSystem(INGameRenderer renderer)
	{
		_renderer = renderer;
	}


	IEnumerable<Type> ISystem.RequiredComponents => new[] { typeof(Transform), typeof(TextRenderer) };


	void ISystem.Add(Entity entity)
	{
		var transform = entity.GetRequiredComponent<Transform>();
		var spriteRenderer = entity.GetRequiredComponent<TextRenderer>();

		var sprite = spriteRenderer.Text;

		var data = new Data(transform, sprite);
		_datas.Add(data);
	}


	void IDrawable.Draw(GameTime gameTime)
	{
		foreach (var data in _datas)
		{
			if (data.Text == null) continue;

			_renderer.Draw(data.Text, data.Transform);
		}
	}



	private class Data
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