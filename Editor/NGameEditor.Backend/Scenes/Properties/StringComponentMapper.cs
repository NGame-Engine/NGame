using System.Reflection;
using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Backend.Scenes.Properties;



public class StringComponentMapper : IComponentMapper
{
	public string TypeIdentifier => typeof(string).FullName!;


	public PropertyDescription Map(PropertyInfo propertyInfo, object obj)
	{
		var rawValue = propertyInfo.GetValue(obj)!;
		var value = (string)rawValue;

		return new PropertyDescription
		{
			Name = propertyInfo.Name,
			TypeIdentifier = TypeIdentifier,
			SerializedValue = value
		};
	}
}
