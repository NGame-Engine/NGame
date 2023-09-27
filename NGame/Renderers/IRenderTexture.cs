namespace NGame.Renderers;

public interface IRenderTexture
{
	void SetPixels(byte[] pixels);
	byte[] GetPixels();
}
