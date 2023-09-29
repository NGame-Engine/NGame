using NGame.Sprites;
using NGame.Transforms;

namespace NGame.Renderers;

public interface INGameRenderer
{
	void Initialize();

	bool BeginDraw();
	void Draw(Sprite sprite, Transform transform);
	void Draw(Line line);
	void EndDraw(bool shouldPresent);
}
