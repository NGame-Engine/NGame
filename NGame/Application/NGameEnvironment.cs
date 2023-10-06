using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using NGame.Setup;

namespace NGame.Application;



internal class NGameEnvironment : INGameEnvironment
{
	public NGameEnvironment(IHostEnvironment hostEnvironment)
	{
		EnvironmentName = hostEnvironment.EnvironmentName;
		ApplicationName = hostEnvironment.ApplicationName;
		ContentRootPath = hostEnvironment.ContentRootPath;
		ContentRootFileProvider = hostEnvironment.ContentRootFileProvider;
		Platform = PlatformFinder.GetPlatform();
	}


	public string EnvironmentName { get; set; }
	public string ApplicationName { get; set; }
	public string ContentRootPath { get; set; }
	public IFileProvider ContentRootFileProvider { get; set; }
	public Platform Platform { get; }
}
