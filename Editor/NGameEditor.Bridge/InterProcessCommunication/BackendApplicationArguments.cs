using Singulink.IO;

namespace NGameEditor.Bridge.InterProcessCommunication;



public record BackendApplicationArguments(
	int FrontendPort,
	IAbsoluteFilePath SolutionFilePath
);
