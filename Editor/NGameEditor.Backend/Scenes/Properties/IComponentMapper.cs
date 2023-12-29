using System.Reflection;
using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Backend.Scenes.Properties;



public interface IComponentMapper
{
	string TypeIdentifier { get; }

	PropertyDescription Map(PropertyInfo propertyInfo, object obj);
}
