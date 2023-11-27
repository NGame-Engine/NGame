using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Cli;
using NGame.Cli.Abstractions;
using NGame.Cli.PackAssets;
using NGame.Setup;

var commands = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
{
	["pack"] = () => RunPackAssetsCommand(args)
};


ProgramRunner.Run(commands, args);
return;


static void RunPackAssetsCommand(string[] args)
{
	var builder = Host.CreateApplicationBuilder(args);


	builder.Services.AddNGameCommon();
	builder.InstallPackAssetsCommand();


	var host = builder.Build();


	var commandRunner = host.Services.GetRequiredService<ICommandRunner>();
	commandRunner.Run();
}
