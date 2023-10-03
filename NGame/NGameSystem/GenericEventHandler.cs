namespace NGame.NGameSystem;



public delegate void GenericEventHandler<TSender, TEventArgs>(
	TSender sender,
	TEventArgs eventArgs
);
