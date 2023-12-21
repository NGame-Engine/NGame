using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGameEditor.Backend.UserInterface;



public static class UserInterfaceInstaller
{
	public static void AddUserInterface(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<ICustomEditorListener, CustomEditorListener>();
	}
}
