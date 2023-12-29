using System.Reflection;
using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Backend.Scenes.Properties;



public class IntComponentMapper : IComponentMapper
{
	public string TypeIdentifier => typeof(int).FullName!;


	public PropertyDescription Map(PropertyInfo propertyInfo, object obj)
	{
		var rawValue = propertyInfo.GetValue(obj)!;
		var value = (int)rawValue;

		return new PropertyDescription
		{
			Name = propertyInfo.Name,
			TypeIdentifier = TypeIdentifier,
			SerializedValue = value.ToString()
		};
	}
}
