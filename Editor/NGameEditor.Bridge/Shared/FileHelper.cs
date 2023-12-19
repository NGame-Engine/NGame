namespace NGameEditor.Bridge.Shared;



public static class FileHelper
{
	public static void SaveFileContentViaIntermediate(string content, string filePath)
	{
		var directory = Path.GetDirectoryName(filePath)!;
		var fileName = Path.GetFileName(filePath);

		var underscoreFileName = $"_{fileName}";
		var fullUnderscoreFileName = Path.Combine(directory, underscoreFileName);

		File.WriteAllText(fullUnderscoreFileName, content);
		File.Move(fullUnderscoreFileName, filePath, true);
	}
}
