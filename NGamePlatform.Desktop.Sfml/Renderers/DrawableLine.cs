using NGame.Components.Renderer2Ds;
using SFML.Graphics;
using Transform = NGame.Components.Transforms.Transform;

namespace NGamePlatform.Desktop.Sfml.Renderers;



public class DrawableLine : SfmlDrawable
{
	private readonly LineRenderer _lineRenderer;


	public DrawableLine(Transform transform, LineRenderer lineRenderer)
		: base(transform)
	{
		_lineRenderer = lineRenderer;
	}


	public override void Draw(RenderWindow renderWindow)
	{
		var line = _lineRenderer.Line;
		if (line == null) return;


		var vertices =
			line
				.Vertices
				.Select(
					x => new Vertex(
						x.ToSfmlVector2YInverted(),
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
