namespace NGame.Components.Physics2D;



public class Collision2DEventArgs : EventArgs
{
	public Collider2D Sender;
	public Collider2D Other;


	public Collision2DEventArgs(Collider2D sender, Collider2D other)
	{
		Sender = sender;
		Other = other;
	}
}
