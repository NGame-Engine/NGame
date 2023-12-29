using NGameEditor.Results;

namespace NGameEditor.Backend.UserInterface;



public interface IValueUpdater
{
	Result SetValue(string? serializedNewValue);
}



public class ValueUpdater(Func<string?, Result> applyValue) : IValueUpdater
{
	public Result SetValue(string? serializedNewValue) => applyValue(serializedNewValue);
}
