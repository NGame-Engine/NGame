namespace NGame.Assets;

public interface IAssetSerializer<T>
{
    T Deserialize(string filePath);
}
