using System.Numerics;
using NGame.Components.Renderer2Ds;
using SFML.Graphics;
using SFML.System;
using Transform = NGame.Components.Transforms.Transform;

namespace NGamePlatform.Desktop.Sfml.Renderers;



public class DrawableLine : SfmlDrawable
{
	private readonly LineRenderer _lineRenderer;
	private readonly SfmlDesktopConfiguration _configuration;


	public DrawableLine(
		Transform transform,
		LineRenderer lineRenderer,
		SfmlDesktopConfiguration configuration
	)
		: base(transform)
	{
		_lineRenderer = lineRenderer;
		_configuration = configuration;
	}


	public override void Draw(RenderWindow renderWindow)
	{
		var line = _lineRenderer.Line;
		if (line == null) return;


		var position = Transform.Position;
		var basePosition = new Vector2(position.X, position.Y);

		Vector2f GetPosition(Vector2 vertexValue)
		{
			var translated = basePosition + vertexValue;
			var finalPosition = translated * _configuration.PixelPerUnit;
			return finalPosition.ToSfmlVector2YInverted();
		}

		var vertices =
			line
				.Vertices
				.Select(
					x => new Vertex(
						GetPosition(x),
						line.Color.ToSfmlColor()
					)
				)
				.ToArray();


		for (int i = 0; i < vertices.Length - 1; i++)
		{
			var firstVertex = vertices[i];
			var secondVertex = vertices[i + 1];


			renderWindow.Draw(
				new[] { firstVertex, secondVertex },
				PrimitiveType.Lines
			);
		}
	}
}
