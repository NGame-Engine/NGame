﻿using NGame.Components.Sprites;
using NGame.Components.Texts;
using NGame.Components.Transforms;

namespace NGame.Renderers;

public interface INGameRenderer
{
	bool BeginDraw();
	void Draw(Sprite sprite, Transform transform);
	void Draw(Line line);
	void Draw(Text text, Transform transform);
	void EndDraw(bool shouldPresent);
}
