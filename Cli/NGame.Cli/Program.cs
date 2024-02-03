using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Cli;
using NGame.Cli.Abstractions;
using NGame.Cli.FindUsedAssets;
using NGame.Cli.PackAssets;
using NGame.Tooling.Setup;

var commands = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
{
	["pack"] = () => RunPackAssetsCommand(args),
	["findusedassets"] = () => RunFindUsedAssetsCommand(args)
};


ProgramRunner.Run(commands, args);
return;


static void RunPackAssetsCommand(string[] args)
{
	var builder = Host.CreateApplicationBuilder(args);


	builder.AddNGameCommon();
	builder.InstallPackAssetsCommand();


	var host = builder.Build();


	var commandRunner = host.Services.GetRequiredService<ICommandRunner>();
	commandRunner.Run();
}


static void RunFindUsedAssetsCommand(string[] args)
{
	var builder = Host.CreateApplicationBuilder(args);


	builder.AddNGameCommon();
	builder.InstallFindUsedAssetsCommand();


	var host = builder.Build();


	var commandRunner = host.Services.GetRequiredService<ICommandRunner>();
	commandRunner.Run();
}
