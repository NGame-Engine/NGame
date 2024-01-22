using System.Text.Json;
using NGame.Assets;
using NGameEditor.Backend.Files;
using NGameEditor.Bridge.Shared;
using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;

namespace NGameEditor.Backend.UserInterface.ValueEditors;



internal class AssetValueEditorFactory(
	IAssetFileWatcher assetFileWatcher
) : IValueEditorFactory
{
	public bool CanHandleType(Type type) => type.IsAssignableTo(typeof(Asset));


	public EditorElement Create(Type type, object? value, Action<object?> setValue)
	{
		var uiElementId = Guid.NewGuid();

		var asset = (Asset?)value;
		var filePath = GetFilePath(type, asset);

		var jsonAssetInfo =
			new JsonAssetInfo
			{
				TypeName = AssetAttribute.GetName(type),
				TypeIdentifier = AssetAttribute.GetDiscriminator(type),
				SelectedFilePath = filePath?.Path
			};

		return new EditorElement(
			new UiElementDto(
				uiElementId,
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

					return
						ParseGuid(x)
							.Then(GetAsset)
							.Then(setValue);
				}
			),
			[]
		);
	}


	private Result<Guid> ParseGuid(string input) =>
		Guid.TryParse(input, out var id) == false
			? Result.Error($"Unable to parse GUID '{input}'")
			: Result.Success(id);


	private Result<Asset> GetAsset(Guid id)
	{
		throw new NotImplementedException();
	}


	private AbsolutePath? GetFilePath(Type assetType, Asset? asset)
	{
		if (asset == null) return null;

		var assetId = asset.Id.Id;

		return
			assetFileWatcher
				.GetAssetsOfType(AssetAttribute.GetDiscriminator(assetType))
				.Then(x => x.First(y => y.Id == assetId))
				.SuccessValue!
				.FilePath;
	}
}
