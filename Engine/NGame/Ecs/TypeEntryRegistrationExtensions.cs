using Microsoft.Extensions.DependencyInjection;
using NGame.Setup;

namespace NGame.Ecs;



public static class TypeEntryRegistrationExtensions
{
	public static INGameBuilder RegisterComponentType<T>(this INGameBuilder builder)
		where T : EntityComponent
	{
		builder.Services.AddSingleton(ComponentTypeEntry.Create<T>());

		return builder;
	}
}
