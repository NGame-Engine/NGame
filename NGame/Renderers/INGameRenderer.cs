using NGame.UpdateSchedulers;

namespace NGame.Renderers;

public interface INGameRenderer
{
	void Initialize();
	void SetPixelsPointer(IntPtr pixelsPointer);

	bool BeginDraw();
	void Draw(GameTime drawLoopTime);
	void EndDraw(bool shouldPresent);
}
