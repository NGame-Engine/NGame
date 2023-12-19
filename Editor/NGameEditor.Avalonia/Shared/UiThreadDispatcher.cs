using System;
using Avalonia.Threading;
using NGameEditor.Functionality.Shared;

namespace NGameEditor.Avalonia.Shared;



public class UiThreadDispatcher(IDispatcher dispatcher) : IUiThreadDispatcher
{
	public void DoOnUiThread(Action action)
	{
		dispatcher.Post(action);
	}
}
