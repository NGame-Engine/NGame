using NGameEditor.Bridge.Shared;

namespace NGameEditor.Bridge.InterProcessCommunication;



public record BackendApplicationArguments(
	int FrontendPort,
	CompatibleAbsolutePath SolutionFilePath
);
