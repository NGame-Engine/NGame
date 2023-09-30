﻿using System.Numerics;

namespace NGame.Texts;

public class Text
{
	public Font Font { get; set; }
	public string Content { get; set; }
	public uint CharacterSize { get; set; }
	public Vector2 TransformOrigin { get; set; }
}