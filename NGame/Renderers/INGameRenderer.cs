using NGame.Sprites;
using NGame.Transforms;

namespace NGame.Renderers;

public interface INGameRenderer
{
	void Initialize();

	bool BeginDraw();
	void Draw(Sprite sprite, Transform transform);
	void Draw(Line line);
	void Draw(Text text, Transform transform);
	void EndDraw(bool shouldPresent);
}
