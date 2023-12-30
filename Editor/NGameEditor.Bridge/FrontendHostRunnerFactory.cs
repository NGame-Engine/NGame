using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Bridge;



public interface IFrontendHostRunnerFactory
{
	IHostRunner Create();
}



public class FrontendHostRunnerRunnerFactory(
	IFrontendApi frontendApi,
	IHostRunnerFactory hostRunnerFactory
) : IFrontendHostRunnerFactory
{
	public IHostRunner Create()
	{
		var hostRunner = hostRunnerFactory.Create();
		hostRunner.AddService(frontendApi);
		hostRunner.Open();
		return hostRunner;
	}
}
