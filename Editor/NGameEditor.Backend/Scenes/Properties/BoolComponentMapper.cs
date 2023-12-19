using System.Reflection;
using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Backend.Scenes.Properties;



public class BoolComponentMapper : IComponentMapper
{
	public string TypeIdentifier => typeof(bool).FullName!;


	public PropertyDescription Map(PropertyInfo propertyInfo, object obj)
	{
		var rawValue = propertyInfo.GetValue(obj)!;
		var value = (bool)rawValue;

		return new PropertyDescription
		{
			Name = propertyInfo.Name,
			TypeIdentifier = TypeIdentifier,
			SerializedValue = value.ToString()
		};
	}
}
