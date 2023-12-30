using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Backend.InterProcessCommunication;



public interface IFrontendApiFactory
{
	IFrontendApi Create();
}



public class FrontendApiFactory(
	BackendApplicationArguments backendApplicationArguments,
	IClientRunner<IFrontendApi> clientRunner
) : IFrontendApiFactory
{
	public IFrontendApi Create()
	{
		var port = backendApplicationArguments.FrontendPort;
		clientRunner.StartClient(port);
		return clientRunner.GetClientService().SuccessValue!;
	}
}
