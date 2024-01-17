using System.Text.Json;
using NGame.Assets;
using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;

namespace NGameEditor.Backend.UserInterface.ValueEditors;



public class AssetValueEditorFactory : IValueEditorFactory
{
	public bool CanHandleType(Type type) => type.IsAssignableTo(typeof(Asset));


	public EditorElement Create(object? value, Action<object?> setValue)
	{
		var id = Guid.NewGuid();

		var jsonAssetInfo = 
			new JsonAssetInfo
		{
			TypeName = "MyTypeName",
			TypeIdentifier = "MyTypeIdentifier",
			SelectedFilePath = null
		};

		return new EditorElement(
			new UiElementDto(
				id,
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
}
