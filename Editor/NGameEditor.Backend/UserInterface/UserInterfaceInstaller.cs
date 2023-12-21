using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Bridge.UserInterface;

namespace NGameEditor.Backend.UserInterface;



public class ColorValueUi(Color color)
{
	private Color Color { get; set; } = color;


	public bool CanHandle(Type valueType) =>
		valueType == typeof(Color);


	public IUiElement CreateUiElement(Color color)
	{
		Action<string> a = x => Color = Color.FromArgb(byte.Parse(x), Color.R, Color.G, Color.B);
		Action<string> r = x => Color = Color.FromArgb(Color.A, byte.Parse(x), Color.G, Color.B);
		Action<string> g = x => Color = Color.FromArgb(Color.A, Color.R, byte.Parse(x), Color.B);
		Action<string> b = x => Color = Color.FromArgb(Color.A, Color.R, Color.G, byte.Parse(x));

		return new StackPanel(
			[
				//new StringEditor()
			]
		);
	}
}



public static class UserInterfaceInstaller
{
	public static void AddUserInterface(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<ICustomEditorListener, CustomEditorListener>();

		builder.Services.AddTransient<IDeserializerProvider, DeserializerProvider>();
		builder.Services.AddTransient<IValueDeserializer, BoolDeserializer>();
	}
}



public interface IValueDeserializer
{
	Type Type { get; }

	object? Deserialize(string serialized);
}



public class BoolDeserializer : IValueDeserializer
{
	public Type Type { get; } = typeof(bool);


	public object? Deserialize(string serialized)
	{
		return bool.Parse(serialized);
	}
}



public interface IDeserializerProvider
{
	IValueDeserializer? Get(Type type);
}



public class DeserializerProvider : IDeserializerProvider
{
	private readonly Dictionary<Type, IValueDeserializer> _deserializers;


	public DeserializerProvider(IEnumerable<IValueDeserializer> deserializers)
	{
		_deserializers = deserializers.ToDictionary(x => x.Type);
	}


	public IValueDeserializer? Get(Type type) =>
		_deserializers.GetValueOrDefault(type);
}
