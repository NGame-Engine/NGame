namespace NGamePlugin.Physics2D.Aether;



public class BiDirectionalLookup<TLeft, TRight> where TLeft : notnull where TRight : notnull
{
	private readonly Dictionary<TLeft, TRight> _rightByLeftIndex = new();
	private readonly Dictionary<TRight, TLeft> _leftByRightIndex = new();


	public void Add(TLeft left, TRight right)
	{
		_rightByLeftIndex[left] = right;
		_leftByRightIndex[right] = left;
	}


	public TRight this[TLeft left] => _rightByLeftIndex[left];
	public TLeft this[TRight right] => _leftByRightIndex[right];


	public void Remove(TLeft left)
	{
		var right = _rightByLeftIndex[left];
		_rightByLeftIndex.Remove(left);
		_leftByRightIndex.Remove(right);
	}


	public void Remove(TRight right)
	{
		var left = _leftByRightIndex[right];
		_leftByRightIndex.Remove(right);
		_rightByLeftIndex.Remove(left);
	}
}
