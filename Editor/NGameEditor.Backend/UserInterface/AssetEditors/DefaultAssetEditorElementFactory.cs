using NGame.Assets;
using NGameEditor.Backend.UserInterface.ValueEditors;

namespace NGameEditor.Backend.UserInterface.AssetEditors;



public interface IDefaultAssetEditorElementFactory : IAssetEditorElementFactory;



class DefaultAssetEditorElementFactory(
	IEnumerable<IValueEditorFactory> valueEditorFactories
) : IDefaultAssetEditorElementFactory
{
	public Type Type => typeof(Asset);


	public IEnumerable<EditorElement> Create(Asset asset, Action<Asset> saveAsset)
	{
		var editablePropertyInfos =
			asset
				.GetType()
				.GetProperties()
				.Where(x => x.CanRead && x.CanWrite);

		var editorElements = new List<EditorElement>();

		foreach (var propertyInfo in editablePropertyInfos)
		{
			var propertyType = propertyInfo.PropertyType;

			var factory = valueEditorFactories
				.FirstOrDefault(x => x.CanHandleType(propertyType));

			if (factory == null)
			{
				continue;
			}


			var editorElement = factory.Create(
				propertyType,
				() => propertyInfo.GetValue(asset),
				x =>
				{
					propertyInfo.SetValue(asset, x);
					saveAsset(asset);
				});

			editorElements.Add(editorElement);
		}

		return editorElements;
	}
}
