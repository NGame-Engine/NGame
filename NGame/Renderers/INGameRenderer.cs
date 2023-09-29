using NGame.Sprites;
using NGame.UpdateSchedulers;

namespace NGame.Renderers;

public interface INGameRenderer
{
	void Initialize();

	bool BeginDraw();
	void Draw(GameTime drawLoopTime);
	void EndDraw(bool shouldPresent);
	void Add(RendererSprite sprite);
}
