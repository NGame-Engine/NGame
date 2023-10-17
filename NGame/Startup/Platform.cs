using System.Runtime.InteropServices;

namespace NGame.Startup;



public class Platform
{
	public Platform(OperatingSystem operatingSystem, Version version)
	{
		OperatingSystem = operatingSystem;
		Version = version;
	}


	public OperatingSystem OperatingSystem { get; }
	public Version Version { get; }


	public bool IsMobile() =>
		OperatingSystem == OperatingSystem.Android ||
		OperatingSystem == OperatingSystem.Ios;
}



public enum OperatingSystem
{
	Unknown,
	Windows,
	MacOs,
	Linux,
	Android,
	Ios
}



public static class PlatformFinder
{
	public static Platform GetPlatform() =>
		new Platform(
			FindOperatingSystem(),
			FindVersion()
		);


	private static OperatingSystem FindOperatingSystem()
	{
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return OperatingSystem.Windows;
		if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return OperatingSystem.MacOs;
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return OperatingSystem.Linux;
		return OperatingSystem.Unknown;
	}


	private static Version FindVersion() => new(0, 0, 0);
}
