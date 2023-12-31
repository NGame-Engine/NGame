using System;
using NGameEditor.ViewModels.LauncherWindows.Logs;

namespace NGameEditor.Avalonia.LauncherWindows.Logs;



public static class LauncherLogDesignData
{
	public static LauncherLogViewModel Example { get; } =
		new()
		{
			LogEntries =
			{
				new LauncherLogEntryViewModel(
					"This is a message",
					DateTime.UnixEpoch
				),
				new LauncherLogEntryViewModel(
					"This is another message",
					DateTime.UnixEpoch
				),
				new LauncherLogEntryViewModel(
					"This is a long message. Lorem ipsum dolor sit amet consectetur " +
					"adipisicing elit. Maxime mollitia,\nmolestiae quas vel sint commodi " +
					"repudiandae consequuntur voluptatum laborum\nnumquam blanditiis " +
					"harum quisquam eius sed odit fugiat iusto fuga praesentium\noptio, " +
					"eaque rerum! Provident similique accusantium nemo autem. Veritatis\n" +
					"obcaecati tenetur iure eius earum ut molestias architecto voluptate " +
					"aliquam\nnihil, eveniet aliquid culpa officia aut!",
					DateTime.UnixEpoch
				)
			}
		};
}
