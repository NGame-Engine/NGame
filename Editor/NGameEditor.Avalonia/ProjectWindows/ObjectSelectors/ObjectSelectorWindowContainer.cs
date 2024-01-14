using System;
using NGameEditor.Functionality.Windows.ProjectWindow;
using NGameEditor.Functionality.Windows.ProjectWindow.Inspector;

namespace NGameEditor.Avalonia.ProjectWindows.ObjectSelectors;



public class ObjectSelectorWindowContainer(
	Func<ObjectSelectorWindow> factoryMethod,
	IProjectWindow projectWindow
) : IObjectSelectorWindow
{
	private ObjectSelectorWindow? ObjectSelectorWindow { get; set; }


	public void Open()
	{
		if (projectWindow.IsActive == false) return;


		if (ObjectSelectorWindow == null)
		{
			ObjectSelectorWindow = factoryMethod();
			ObjectSelectorWindow.Closed += (_, _) => Close();
			projectWindow.Closing += () => ObjectSelectorWindow?.Close();
		}

		ObjectSelectorWindow.Show();
	}


	private void Close()
	{
		if (ObjectSelectorWindow == null) return;

		ObjectSelectorWindow = null;
	}
}
