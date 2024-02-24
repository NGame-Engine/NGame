namespace NGame.Assets.Packer.AssetOverviews;



public record AssetEntry(
	Guid Id,
	PathInfo MainPathInfo,
	string PackageName,
	PathInfo? SatellitePathInfo
);
