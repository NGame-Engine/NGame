namespace NGame.Cli;



public static class ProgramRunner
{
	public static void Run(Dictionary<string, Action> commands, string[] args)
	{
		var command = args.FirstOrDefault();

		if (
			command == null ||
			!commands.TryGetValue(command, out Action? runCommand)
		)
		{
			var commandStrings = $"{string.Join(Environment.NewLine, commands.Values)}";
			throw new InvalidOperationException(
				$"Unable to find command '{command}', possible values are {commandStrings}");
		}


		runCommand();
	}
}
