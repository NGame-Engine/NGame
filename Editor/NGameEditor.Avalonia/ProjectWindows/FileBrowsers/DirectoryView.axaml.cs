﻿using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;

namespace NGameEditor.Avalonia.ProjectWindows.FileBrowsers;



public partial class DirectoryView : UserControl, IView<DirectoryViewModel>
{
	public DirectoryView()
	{
		InitializeComponent();
	}
}
