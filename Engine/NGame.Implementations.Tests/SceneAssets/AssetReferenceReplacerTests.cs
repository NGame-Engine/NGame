using FluentAssertions;
using NGame.Assets;
using NGame.Core.Ecs.SceneAssets;
using NSubstitute;

namespace NGame.Core.Tests.SceneAssets;



[Asset(Discriminator = "Child")]
public class ChildTestAsset : Asset
{
	public int MyValue { get; init; }
}



[Asset(Discriminator = "Parent")]
public class ParentTestAsset : Asset
{
	public ChildTestAsset Child { get; init; } = null!;
	public int ParentValue { get; init; }
}



public class ContainingObject
{
	public ParentTestAsset Parent { get; init; } = null!;
}



public class AssetReferenceReplacerTests
{
	[Fact]
	public void ReplaceAssetReferences_UnassignableReference_IsSkipped()
	{
		var assetId = AssetId.Parse("2FFB5856-A531-4F32-BD86-64267686DDFB");

		var fromPackReader = Substitute.For<IAssetFromPackReader>();
		fromPackReader
			.ReadFromAssetPack(assetId)
			.Returns(new ChildTestAsset { MyValue = 7 });

		var postProcessor = new AssetReferenceReplacer(fromPackReader);

		var containingObject = new
		{
			Obj = new ChildTestAsset { Id = assetId }
		};


		// Act
		postProcessor.ReplaceAssetReferences(containingObject);


		// Assert
		containingObject.Obj.MyValue.Should().NotBe(7);
	}


	[Fact]
	public void ReplaceAssetReferences_ObjectWithAssets_AssignsRealValues()
	{
		var fromPackReader = Substitute.For<IAssetFromPackReader>();
		fromPackReader
			.ReadFromAssetPack(AssetId.Parse("C49E53CE-4197-4A5B-86A7-FFE6F993855F"))
			.Returns(
				new ParentTestAsset
				{
					ParentValue = 6,
					Child = new ChildTestAsset
					{
						Id = AssetId.Parse("2FFB5856-A531-4F32-BD86-64267686DDFB")
					}
				}
			);

		fromPackReader
			.ReadFromAssetPack(AssetId.Parse("2FFB5856-A531-4F32-BD86-64267686DDFB"))
			.Returns(new ChildTestAsset { MyValue = 7 });


		var postProcessor = new AssetReferenceReplacer(fromPackReader);

		var containingObject = new ContainingObject
		{
			Parent = new ParentTestAsset
			{
				Id = AssetId.Parse("C49E53CE-4197-4A5B-86A7-FFE6F993855F")
			}
		};


		// Act
		postProcessor.ReplaceAssetReferences(containingObject);


		// Assert
		var parent = containingObject.Parent;
		parent.ParentValue.Should().Be(6);

		parent.Child.Should().NotBe(null);
		parent.Child.MyValue.Should().Be(7);
	}
}
