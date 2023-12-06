namespace NGame.Assets;



public readonly struct AssetId : IEquatable<AssetId>
{
	public readonly Guid Id;


	public AssetId()
	{
		Id = Guid.NewGuid();
	}


	private AssetId(Guid id)
	{
		Id = id;
	}


	public static AssetId Parse(string input) => new(Guid.Parse(input));


	public static AssetId Create(Guid id) =>
		id == default
			? throw new InvalidOperationException("Empty GUID provided")
			: new AssetId(id);


	public bool Equals(AssetId other)
	{
		return Id.Equals(other.Id);
	}


	public override bool Equals(object? obj)
	{
		return obj is AssetId other && Equals(other);
	}


	public override int GetHashCode()
	{
		return Id.GetHashCode();
	}


	public static bool operator ==(AssetId left, AssetId right)
	{
		return left.Equals(right);
	}


	public static bool operator !=(AssetId left, AssetId right)
	{
		return !left.Equals(right);
	}


	public override string ToString() => $"AssetId: {Id}";
}
