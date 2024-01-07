using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Backend.UserInterface.AssetEditors;
using NGameEditor.Backend.UserInterface.ComponentEditors;
using NGameEditor.Backend.UserInterface.ValueEditors;

namespace NGameEditor.Backend.UserInterface;



public static class UserInterfaceInstaller
{
	public static void AddUserInterface(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<ICustomEditorListener, CustomEditorListener>();
		builder.Services.AddTransient<IDefaultComponentEditorElementFactory, DefaultComponentEditorElementFactory>();
		builder.Services.AddTransient<IDefaultAssetEditorElementFactory, DefaultAssetEditorElementFactory>();

		builder.Services.AddTransient<IValueEditorFactory, BoolValueEditorFactory>();
		builder.Services.AddTransient<IValueEditorFactory, StringValueEditorFactory>();
	}
}
