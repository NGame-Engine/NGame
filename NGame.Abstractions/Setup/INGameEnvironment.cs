using Microsoft.Extensions.Hosting;

namespace NGame.Setup;



public interface INGameEnvironment : IHostEnvironment
{
	Platform Platform { get; }
}
