namespace NGame.Setup;



public class OperatingSystemVersion
{
	public int Major { get; set; }
	public int Minor { get; set; }
}



public enum NgOperatingSystem
{
	Unknown,
	Windows,
	MacOs,
	Linux,
	Android,
	Ios
}



public class PlatformConfiguration
{
	public static readonly string ConfigKey = "Platform";

	public NgOperatingSystem NgOperatingSystem { get; set; }
	public OperatingSystemVersion Version { get; set; } = null!;
}



public static class PlatformConfigurationExtensions
{
	public static bool IsMobile(this PlatformConfiguration platformConfiguration) =>
		platformConfiguration.NgOperatingSystem == NgOperatingSystem.Android ||
		platformConfiguration.NgOperatingSystem == NgOperatingSystem.Ios;
}
