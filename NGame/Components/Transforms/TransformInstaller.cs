using Microsoft.Extensions.DependencyInjection;
using NGame.Application;
using NGame.UpdateSchedulers;

namespace NGame.Components.Transforms;



public static class TransformInstaller
{
	public static INGameApplicationBuilder AddTransforms(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<TransformProcessor>();

		builder.Services.AddSingleton<IMatrixUpdater, MatrixUpdater>();

		return builder;
	}


	public static NGameApplication UseTransforms(this NGameApplication app)
	{
		app.UseDrawable<TransformProcessor>();

		return app;
	}
}
