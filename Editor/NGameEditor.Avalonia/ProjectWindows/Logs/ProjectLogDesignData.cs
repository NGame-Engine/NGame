using System;
using NGameEditor.ViewModels.ProjectWindows.Logs;

namespace NGameEditor.Avalonia.ProjectWindows.Logs;



public static class ProjectLogDesignData
{
	public static LogWindowModel Example { get; } =
		new()
		{
			LogEntries =
			{
				new LogEntryViewModel(
					"First Message",
					DateTime.UnixEpoch
				),
				new LogEntryViewModel(
					"Second Message",
					DateTime.UnixEpoch
				),
				new LogEntryViewModel(
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
