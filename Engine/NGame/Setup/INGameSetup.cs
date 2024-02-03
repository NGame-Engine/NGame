using Microsoft.Extensions.Hosting;

namespace NGame.Setup;



public interface INGameSetup
{
	void ConfigureApplication(IHostApplicationBuilder builder);
}
