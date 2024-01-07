using NGame.Assets;
using NGameEditor.Backend.UserInterface.ValueEditors;

namespace NGameEditor.Backend.UserInterface.AssetEditors;



public interface IDefaultAssetEditorElementFactory : IAssetEditorElementFactory;



class DefaultAssetEditorElementFactory(
	IEnumerable<IValueEditorFactory> valueEditorFactories
) : IDefaultAssetEditorElementFactory
{
	private readonly Dictionary<Type, IValueEditorFactory> _valueEditorFactories =
		valueEditorFactories.ToDictionary(x => x.ValueType);

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

			if (_valueEditorFactories.TryGetValue(propertyType, out var factory) == false)
			{
				continue;
			}


			var editorElement = factory.Create(
				propertyInfo.GetValue(asset),
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
