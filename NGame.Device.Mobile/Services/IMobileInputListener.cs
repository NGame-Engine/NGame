namespace NGame.Services;



public interface IMobileInputListener
{
	event Action<TappedEventArgs> Tapped;
	event Action<SwipedEventArgs> Swiped;
	event Action<PinchEventArgs> Pinched;
	event Action<PanEventArgs> Panned;
}
