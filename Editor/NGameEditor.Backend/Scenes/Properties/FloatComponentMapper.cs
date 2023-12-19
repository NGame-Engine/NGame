using System.Globalization;
using System.Reflection;
using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Backend.Scenes.Properties;



public class FloatComponentMapper : IComponentMapper
{
	public string TypeIdentifier => typeof(float).FullName!;


	public PropertyDescription Map(PropertyInfo propertyInfo, object obj)
	{
		var rawValue = propertyInfo.GetValue(obj)!;
		var value = (float)rawValue;

		return new PropertyDescription
		{
			Name = propertyInfo.Name,
			TypeIdentifier = TypeIdentifier,
			SerializedValue = value.ToString(CultureInfo.InvariantCulture)
		};
	}
}
