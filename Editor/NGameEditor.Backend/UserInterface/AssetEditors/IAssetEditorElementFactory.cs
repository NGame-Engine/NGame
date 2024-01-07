using NGame.Assets;

namespace NGameEditor.Backend.UserInterface.AssetEditors;



public interface IAssetEditorElementFactory
{
	Type Type { get; }
	public IEnumerable<EditorElement> Create(Asset asset, Action<Asset> saveAsset);
}
