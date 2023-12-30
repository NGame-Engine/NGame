using NGameEditor.Backend.Setup.ApplicationConfigurations;
using NGameEditor.Bridge.Frontend;
using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Backend.Ipc.Setup;



public interface IFrontendApiFactory
{
	IFrontendApi Create();
}



public class FrontendApiFactory(
	ApplicationConfiguration applicationConfiguration,
	IClientServiceFactory clientServiceFactory
) : IFrontendApiFactory
{
	public IFrontendApi Create()
	{
		var ipEndPoint = applicationConfiguration.FrontendIpEndPoint;
		return clientServiceFactory.CreateClientService<IFrontendApi>(ipEndPoint);
	}
}
