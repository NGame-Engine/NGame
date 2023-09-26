using NGame.UpdaterSchedulers;

namespace NGame.Renderers;

public interface INGameRenderer
{
	void Initialize();

	Task<bool> BeginDraw();
	Task Draw(GameTime drawLoopTime);
	Task EndDraw(bool shouldPresent);
}
