using System.Text.Json;
using NGame.Assets;
using NGameEditor.Backend.Files;
using NGameEditor.Bridge.Shared;
using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;

namespace NGameEditor.Backend.UserInterface.ValueEditors;



internal class AssetValueEditorFactory(
	IAssetFileWatcher assetFileWatcher,
	IBackendAssetDeserializer backendAssetDeserializer
) : IValueEditorFactory
{
	public bool CanHandleType(Type type) => type.IsAssignableTo(typeof(Asset));


	public EditorElement Create(
		Type type,
		Func<object?> getValue,
		Action<object?> setValue
	)
	{
		var asset = (Asset?)getValue();
		var filePath = GetFilePath(asset);

		var jsonAssetInfo =
			new JsonAssetInfo
			{
				TypeName = AssetAttribute.GetName(type),
				TypeIdentifier = AssetAttribute.GetDiscriminator(type),
				SelectedFilePath = filePath
			};


		return new EditorElement(
			new UiElementDto(
				Guid.NewGuid(),
				UiElementType.Asset,
				JsonSerializer.Serialize(jsonAssetInfo),
				[]
			),
			new ValueUpdater(
				x =>
				{
					if (x == null)
					{
						setValue(null);
						return Result.Success();
					}

					var newValue = JsonSerializer.Deserialize<JsonAssetInfo>(x)!;
					var selectedFilePath = newValue.SelectedFilePath!;

					var readAssetResult = backendAssetDeserializer.ReadAsset(selectedFilePath.ToAbsoluteFilePath());
					if (readAssetResult.HasError)
					{
						return Result.Error(readAssetResult.ErrorValue!);
					}

					var newAsset = readAssetResult.SuccessValue!;
					setValue(newAsset);

					return Result.Success();
				}
			),
			[]
		);
	}


	private AbsolutePath? GetFilePath(Asset? asset)
	{
		if (asset == null) return null;

		var assetId = asset.Id;

		return
			assetFileWatcher
				.GetAssetDescriptions()
				.First(x => x.Id == assetId)
				.FilePath;
	}
}
